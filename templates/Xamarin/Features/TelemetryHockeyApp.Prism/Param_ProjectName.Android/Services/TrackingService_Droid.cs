using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using HockeyApp;

using Param_ItemNamespace.Services;
using Param_ItemNamespace.Droid.Services;
using System.ComponentModel;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(TrackingService_Droid))]

namespace Param_ItemNamespace.Droid.Services
{
    public class TrackingService_Droid : TrackingServiceBase, ITrackingService
    {
        public TrackingService_Droid()
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
                                var metricMeasureTuple = Aggregrate(item.Key);
                                MetricsManager.TrackEvent($"Metric:{item.Key}", metricMeasureTuple.Item1, metricMeasureTuple.Item2);
                            }
                        }
                    }
                }
            };
            thread.RunWorkerAsync();
        }

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

        public void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            if (AppLoggingLevel >= TrackDependencyLogLevel)
            {
                MetricsManager.TrackEvent($"Dependency:{dependencyName} - Command:{commandName}", new Dictionary<string, string>
                {
                    {"DependencyName",  dependencyName},
                    {"CommandName", commandName },

                },
                new Dictionary<string, double>
                {
                    {"Duration", duration.TotalMilliseconds }
                });
            }
        }

        public void TrackEvent(string eventName)
        {
            if (AppLoggingLevel >= TrackEventLogLevel)
                MetricsManager.TrackEvent(eventName);
        }

        public void TrackEvent(string eventName, TrackingLoggingLevel loggingLevel = TrackingLoggingLevel.Information)
        {
            if (AppLoggingLevel >= TrackEventLogLevel)
                MetricsManager.TrackEvent(eventName);
        }

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
                    MetricsManager.TrackEvent($"Exception:{exception.Message}", eventProperties, null);
                }
            }
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties)
        {
            if (AppLoggingLevel >= TrackMetricLogLevel)
            {
                var measurement = new Dictionary<string, double>
                {
                    {name,  value}
                };

                MetricsManager.TrackEvent($"Metric:{name}", properties != null ? new Dictionary<string, string>(properties) : null, measurement);
            }
        }

        public void TrackPageView(string name)
        {
            if (AppLoggingLevel >= TrackPageViewLogLevel)
            {
                MetricsManager.TrackEvent("Page View", new Dictionary<string, string>
                {
                    {"PageName", name }
                }, null);
            }
        }

        private Tuple<Dictionary<string, string>, Dictionary<string, double>> Aggregrate(string name)
        {
            var metric = new Dictionary<string, string>()
            {
                {"Name", name },
                {"Timestamp",  DateTime.Now.ToString()}
            };

            if (TrackingServiceMetricCollection[name]?.MetricProperties != null)
            {
                foreach (var item in TrackingServiceMetricCollection[name].MetricProperties)
                {
                    metric.Add(item.Key, item.Value);
                }
            }

            var measurement = new Dictionary<string, double>()
            {
                {"Count",  TrackingServiceMetricCollection[name].Measurements.Count},
                {"Max",  TrackingServiceMetricCollection[name].Measurements.Max()},
                {"Min", TrackingServiceMetricCollection[name].Measurements.Min()},
                {"MetricAvg", TrackingServiceMetricCollection[name].Measurements.Average()}
            };
            var mean = TrackingServiceMetricCollection[name].Measurements.Sum() / metric.Count;
            measurement.Add("StandardDeviation", Math.Sqrt((double)(TrackingServiceMetricCollection[name].Measurements.Sum(v => { var diff = v - mean; return diff * diff; }) / metric.Count)));

            TrackingServiceMetricCollection[name].Start = DateTime.Now; // sets new time and clears out the collection
            return Tuple.Create(metric, measurement);
        }
    }
}