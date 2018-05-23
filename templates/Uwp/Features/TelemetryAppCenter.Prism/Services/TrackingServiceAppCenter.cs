using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using System.ComponentModel;
using System.Diagnostics;

namespace Param_ItemNamespace.Services
{
    public class TrackingServiceAppCenter : TrackingServiceBase, ITrackingService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TrackingServiceAppCenter()
        {
            thread = new BackgroundWorker();
            thread.DoWork += async (o, e) =>
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
                                Analytics.TrackEvent($"Metric:{item.Key}", Aggregrate(item.Key));
                            }
                        }
                    }
                }
            };
            thread.RunWorkerAsync();
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
                Analytics.TrackEvent($"Dependency:{dependencyName} - Command:{commandName}", new Dictionary<string, string>
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
                Analytics.TrackEvent(eventName);
        }

        /// <summary>
        /// Tracks custom events.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="loggingLevel"></param>   
        public void TrackEvent(string eventName, TrackingLoggingLevel loggingLevel = TrackingLoggingLevel.Warning)
        {
            if (AppLoggingLevel >= TrackEventLogLevel)
                Analytics.TrackEvent(eventName);
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
                    Analytics.TrackEvent($"Exception:{exception.Message}", eventProperties);
                }
            }
        }

        private IDictionary<string, string> Aggregrate(string name)
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

                Analytics.TrackEvent($"Metric:{name}", eventProperties);
            }
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
        /// Tracks PageViews
        /// </summary>
        /// <param name="name"></param>    
        public void TrackPageView(string name)
        {
            if (AppLoggingLevel >= TrackPageViewLogLevel)
            {
                Analytics.TrackEvent("Page View", new Dictionary<string, string>
                {
                    {"PageName", name }
                });
            }
        }
    }
}
