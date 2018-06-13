using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Param_ItemNamespace.Services
{
    public class TrackingService :  ITrackingService
    {
        public TrackingLoggingLevel AppLoggingLevel { get; set; } = TrackingLoggingLevel.Information;
        public TrackingLoggingLevel TrackEventLogLevel { get; set; } = TrackingLoggingLevel.Information;
        public TrackingLoggingLevel TrackDependencyLogLevel { get; set; } = TrackingLoggingLevel.Information;
        public TrackingLoggingLevel TrackPageViewLogLevel { get; set; } = TrackingLoggingLevel.Information;
        public TrackingLoggingLevel TrackExceptionLogLevel { get; set; } = TrackingLoggingLevel.Error;
        public TrackingLoggingLevel TrackMetricLogLevel { get; set; } = TrackingLoggingLevel.Warning;
        public int AggregateMetricLoggingDelay { get; set; } = 60000;
        protected readonly Dictionary<string, TrackingServiceMetric> TrackingServiceMetricCollection = new Dictionary<string, TrackingServiceMetric>();
        public BackgroundWorker thread;
        protected readonly Boolean stop = false;
        protected readonly object _lockObject = new object();
        protected ITrackingAPI TrackingAPI { get; private set; }

        public TrackingService(ITrackingAPI trackingAPI )
        { 
            TrackingAPI = trackingAPI;
            thread = new BackgroundWorker();
            thread.DoWork += async(o, e) =>
            {
                while (!stop)
                {
                    await Task.Delay(AggregateMetricLoggingDelay).ConfigureAwait(false);
                    lock (_lockObject)
                    {
                        foreach (var item in TrackingServiceMetricCollection)
                        {
                            if (item.Value.Measurements.Count > 0 && item.Value.Start.Add(item.Value.MetricTimeSpan) <= DateTime.Now && AppLoggingLevel >= item.Value.MetricLoggingLevel)
                            {
                                Debug.WriteLine($"TrackingMetric sent - metric:{item.Key}, Number of Measurements:{ item.Value.Measurements.Count}");
                                TrackingAPI.TrackEvent($"Metric:{item.Key}", Aggregrate(item.Key));
                            }
                        }
                    }
                }
            };
            thread.RunWorkerAsync();
        }
        
        /// <summary>
        /// Tracks a metric of aggregated data for a specific timespan
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        /// <param name="duration"></param>
        /// <param name="loggingLevel"></param>
        public void TrackAggregateMetric(string name, double value, IDictionary<string, string> properties, TimeSpan duration, TrackingLoggingLevel loggingLevel = TrackingLoggingLevel.Warning)
        {
            lock (_lockObject)
            {
                if (!TrackingServiceMetricCollection.ContainsKey(name))
                {
                    TrackingServiceMetricCollection.Add(name, new TrackingServiceMetric() { Start = DateTime.Now, MetricTimeSpan = duration, MetricLoggingLevel = loggingLevel, MetricProperties = properties });
                }
                TrackingServiceMetricCollection[name].Value = value;
            }
        }

        /// <summary>
        /// Tracks a service call and tracks timing
        /// </summary>
        /// <param name="dependencyName"></param>
        /// <param name="commandName"></param>
        /// <param name="startTime"></param>
        /// <param name="duration"></param>
        /// <param name="success"></param>
        public void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            if (AppLoggingLevel >= TrackDependencyLogLevel)
            {
                TrackingAPI.TrackEvent($"Dependency:{dependencyName} - Command:{commandName}", new Dictionary<string, string>
                {
                    {"DependencyName",  dependencyName},
                    {"CommandName", commandName },
                    {"Duration", duration.ToString() }
                });
            }
        }

        /// <summary>
        /// Tracks custom events.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public void TrackEvent(string eventName)
        {
            if (AppLoggingLevel >= TrackEventLogLevel)
            {
                TrackingAPI.TrackEvent(eventName);
            }
        }

        /// <summary>
        /// Tracks custom events.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="loggingLevel"></param>   
        public void TrackEvent(string eventName, TrackingLoggingLevel loggingLevel = TrackingLoggingLevel.Warning)
        {
            if (AppLoggingLevel >= TrackEventLogLevel)
                TrackingAPI.TrackEvent(eventName);
        }


        /// <summary>
        /// Tracks custom events.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="loggingLevel"></param>   
        public void TrackEvent(string eventName, IDictionary<string, string> properties)
        {
            if (AppLoggingLevel >= TrackEventLogLevel)
                TrackingAPI.TrackEvent(eventName, properties);
        }

        /// <summary>
        /// Tracks Exceptions
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="properties"></param>
        public void TrackException(Exception exception, IDictionary<string, string> properties)
        {
            if (exception != null && AppLoggingLevel >= TrackExceptionLogLevel)
            {
                // check if task was cancelled 
                if (exception.GetType() != typeof(TaskCanceledException) || !((TaskCanceledException)(exception)).CancellationToken.IsCancellationRequested)
                {
                    var eventProperties = new Dictionary<string, string>
                    {
                        {"HResult",  exception.HResult.ToString()},
                        {"Message", exception.Message },
                        {"StackTrack", exception.StackTrace }
                    };

                    if (properties != null)
                    {
                        foreach (var property in properties)
                            eventProperties.Add(property.Key, property.Value);
                    }

                    TrackingAPI.TrackException(exception, properties);
                }
            }
        }

        /// <summary>
        /// TrackMetric 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        public void TrackMetric(string name, double value, IDictionary<string, string> properties)
        {
            if (AppLoggingLevel >= TrackMetricLogLevel)
            {
                var eventProperties = new Dictionary<string, string>
                {
                    {name,  value.ToString()}
                };

                if (properties != null)
                {
                    foreach (var property in properties)
                        eventProperties.Add(property.Key, property.Value);
                }
                TrackingAPI.TrackEvent($"Metric:{name}", eventProperties);
            }
        }

        /// <summary>
        /// Tracks PageViews
        /// </summary>
        /// <param name="name"></param> 
        public void TrackPageView(string name)
        {
            if (AppLoggingLevel >= TrackPageViewLogLevel)
            {
                TrackingAPI.TrackEvent("Page View", new Dictionary<string, string>
                {
                    {"Page Name", name }
                });
            }
        }

        /// <summary>
        /// Aggregates values collected in TrackAggregateMetric
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Dictionay of aggregated properties</returns>
        protected  IDictionary<string, string> Aggregrate(string name)
        {
            var metric = new Dictionary<string, string>()
            {
                {"Name", name },
                {"Count",  TrackingServiceMetricCollection[name].Measurements.Count.ToString()},
                {"Max",  TrackingServiceMetricCollection[name].Measurements.Max().ToString()},
                {"Min", TrackingServiceMetricCollection[name].Measurements.Min().ToString()},
                {"Value",  TrackingServiceMetricCollection[name].Measurements.Average().ToString()},
                {"MetricAvg", TrackingServiceMetricCollection[name].Measurements.Average().ToString()},
                {"Timestamp",  DateTime.Now.ToString()}
            };
            var mean = TrackingServiceMetricCollection[name].Measurements.Sum() / metric.Count;
            metric.Add("StandardDeviation", Math.Sqrt((double)(TrackingServiceMetricCollection[name].Measurements.Sum(v => { var diff = v - mean; return diff * diff; }) / metric.Count)).ToString());

            //copy metric properties
            if (TrackingServiceMetricCollection[name] != null && TrackingServiceMetricCollection[name].MetricProperties != null)
            {
                foreach (var item in TrackingServiceMetricCollection[name].MetricProperties)
                {
                    metric.Add(item.Key, item.Value);
                }
            }

            TrackingServiceMetricCollection[name].Start = DateTime.Now; // sets new time and clears out the collection
            return metric;
        }       
    }

    /// <summary>
    /// Metric class for TrackingService. Maintains a collection of measurements
    /// </summary>
    public class TrackingServiceMetric
    {
        private DateTime _Start;
        public List<double> Measurements = new List<double>();

        public DateTime Start
        {
            get { return _Start; }
            set
            {
                _Start = value;
                Measurements.Clear();
            }
        }

        public double Value
        {
            set
            {
                Measurements.Add(value);
            }
        }

        public IDictionary<string, string> MetricProperties { get; set; }
        public TimeSpan MetricTimeSpan { get; set; } = TimeSpan.FromSeconds(60);
        public TrackingLoggingLevel MetricLoggingLevel { get; set; } = TrackingLoggingLevel.Warning;
    }

    /// <summary>
    /// A class for measuring latency of operations.
    /// </summary>
    /// <remarks>
    /// Using struct here to prevent impact on the GC. Pass by value is fine as
    /// the only behavior occurs on dispose. Multiple calls to dispose will
    /// result in multiple latency metrics being tracked, so it is suggested
    /// to use this helper in the context of a <code>using</code> block, e.g.:
    /// 
    /// <code>
    /// using (new TrackedTimedEvent("MyMetric"))
    /// {
    ///    /* Do stuff */
    /// }
    /// </code>
    /// 
    /// Also note that using Stopwatch in a tight loop will impact performance
    /// </remarks>
     struct TrackedTimedEvent : IDisposable
    {
        private readonly Stopwatch s_stopwatch;

        private readonly string _eventName;
        private readonly ITrackingService _trackingService;

        public TrackedTimedEvent(string eventName, ITrackingService trackingService)
        {
            s_stopwatch = new Stopwatch();
            s_stopwatch.Start();
            _eventName = eventName;
            _trackingService = trackingService;
        }

        public void Dispose()
        {
            s_stopwatch.Stop();
            _trackingService.TrackMetric(_eventName, s_stopwatch.ElapsedMilliseconds, new Dictionary<string, string> { { "Elapsed Time (msec)", $"{DateTime.Now.ToString()} : {s_stopwatch.ElapsedMilliseconds.ToString()} msec" } });
        }
    }
}
