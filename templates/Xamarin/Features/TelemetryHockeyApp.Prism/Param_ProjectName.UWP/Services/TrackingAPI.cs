using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Param_ItemNamespace.Services;
using Param_ItemNamespace.UWP.Services;

using Microsoft.HockeyApp;
using Microsoft.HockeyApp.DataContracts;

namespace Param_ItemNamespace.UWP.Services
{
    public class TrackingAPI :  ITrackingAPI
    {
        /// <summary>
        /// Tracks custom events.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public void TrackEvent(string eventName)
        {
            HockeyClient.Current.TrackEvent(eventName);
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties)
        {
            HockeyClient.Current.TrackEvent(eventName, properties);
        }
        public void TrackEvent(string eventName, IDictionary<string, string> properties, IDictionary<string, double> measurement)
        {
            HockeyClient.Current.TrackEvent(eventName, properties, measurement);
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
            HockeyClient.Current.TrackDependency(dependencyName, commandName, startTime, duration, success);
        }

        /// <summary>
        /// Tracks Exceptions
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="properties"></param>        
        public void TrackException(Exception exception, IDictionary<string, string> properties)
        {
           HockeyClient.Current.TrackTrace("Exception - " + exception.Message, SeverityLevel.Error, properties);
        }

         /// <summary>
        /// Tracks a metric of aggregated data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>         
        public void TrackMetric(string name, double value, IDictionary<string, string> properties)
        {
            HockeyClient.Current.TrackMetric(name, value, properties);
        }

        /// <summary>
        /// Tracks PageViews
        /// </summary>
        /// <param name="name"></param>        
        public void TrackPageView(string name)
        {
            HockeyClient.Current.TrackPageView(name);
        }
    }
}
