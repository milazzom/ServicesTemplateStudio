using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Param_ItemNamespace.iOS.Services;
using Param_ItemNamespace.Services;

using HockeyApp;

using Xamarin.Forms;

namespace Param_ItemNamespace.iOS.Services
{
    public class TrackingAPI : ITrackingAPI
    {
        public void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success)
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

        public void TrackEvent(string eventName)
        {
            MetricsManager.TrackEvent(eventName);
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties)
        {
            MetricsManager.TrackEvent(eventName, new Dictionary<string, string>(properties), null);
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties, IDictionary<string, double> measurement)
        {
            MetricsManager.TrackEvent(eventName, new Dictionary<string, string>(properties), new Dictionary<string, double>(measurement));
        }
        
        public void TrackException(Exception exception, IDictionary<string, string> eventProperties)
        {
            MetricsManager.TrackEvent($"Exception:{exception.Message}", new Dictionary<string, string>(eventProperties), null);
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties)
        {
            MetricsManager.TrackEvent($"Metric:{name}", properties != null ? new Dictionary<string, string>(properties) : null, new Dictionary<string, double>() { { "value", value } });
        }

        public void TrackPageView(string name)
        {
            MetricsManager.TrackEvent("Page View", new Dictionary<string, string>
            {
                {"PageName", name }
            }, null);
        }
    }
}
