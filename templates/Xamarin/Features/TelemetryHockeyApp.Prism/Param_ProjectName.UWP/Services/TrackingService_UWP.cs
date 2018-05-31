using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.HockeyApp;
using Microsoft.HockeyApp.DataContracts;
using Param_ItemNamespace.Services;
using Param_ItemNamespace.UWP.Services;

[assembly: Xamarin.Forms.Dependency(typeof(TrackingService_UWP))]
namespace Param_ItemNamespace.UWP.Services
{
     public class TrackingService_UWP : TrackingServiceBase, ITrackingService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TrackingService_UWP()
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
                                HockeyClient.Current.TrackMetric(Aggregrate(item.Key));
                            }
                        }
                    }
                }
            };
            thread.RunWorkerAsync();
        }

        /// <summary>
        /// Tracks custom events.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public void TrackEvent(string eventName)
        {
            if (AppLoggingLevel >= TrackEventLogLevel)
                HockeyClient.Current.TrackEvent(eventName);
        }

        /// <summary>
        /// Tracks custom events.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="loggingLevel"></param>        
        public void TrackEvent(
            string eventName,
             TrackingLoggingLevel loggingLevel = TrackingLoggingLevel.Information)
        {
            if (AppLoggingLevel >= TrackEventLogLevel)
                HockeyClient.Current.TrackEvent(eventName);
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
                HockeyClient.Current.TrackDependency(dependencyName, commandName, startTime, duration, success);
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
                    HockeyClient.Current.TrackTrace("Exception - " + exception.Message, SeverityLevel.Error, properties);
                }
            }
            //Please note, that for UWP application, you will see exception from TrackException method in HockeyApp application as a crash, for all other application types you will see exception from this method in Application Insights portal.
            //HockeyClient.Current.TrackException(exception, properties);
        }

        private MetricTelemetry Aggregrate(string name)
        {
            var metric = new MetricTelemetry
            {
                Name = name,
                Count = TrackingServiceMetricCollection[name].Measurements.Count,
                Max = TrackingServiceMetricCollection[name].Measurements.Max(),
                Min = TrackingServiceMetricCollection[name].Measurements.Min(),
                Value = TrackingServiceMetricCollection[name].Measurements.Average()
            };

            metric.Properties.Add("MetricAvg", TrackingServiceMetricCollection[name].Measurements.Average().ToString());
            //copy metric properties
            if (TrackingServiceMetricCollection[name] != null && TrackingServiceMetricCollection[name].MetricProperties != null)
            {
                foreach (var item in TrackingServiceMetricCollection[name].MetricProperties)
                {
                    metric.Properties.Add(item.Key, item.Value);
                }
            }

            metric.Timestamp = DateTime.Now;
            
            var mean = TrackingServiceMetricCollection[name].Measurements.Sum() / metric.Count;
            metric.StandardDeviation = Math.Sqrt((double)(TrackingServiceMetricCollection[name].Measurements.Sum(v => { var diff = v - mean; return diff * diff; }) / metric.Count));
            TrackingServiceMetricCollection[name].Start = DateTime.Now; // sets new time and clears out the collection
            return metric;
        }

        /// <summary>
        /// Tracks a metric of aggregated data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>         
        public void TrackMetric(string name, double value, IDictionary<string, string> properties)
        {
            if (AppLoggingLevel >= TrackMetricLogLevel)
                HockeyClient.Current.TrackMetric(name, value, properties);
        }

        /// <summary>
        /// Tracks a metric of aggregated data for a specific timespan
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        /// <param name="duration"></param>
        /// <param name="loggingLevel"></param>
        public void TrackAggregateMetric(string name, double value, IDictionary<string, string> properties, TimeSpan duration, TrackingLoggingLevel loggingLevel  = TrackingLoggingLevel.Warning)
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
                HockeyClient.Current.TrackPageView(name);
            }
        }
    }
}
