using System;
using System.Collections.Generic;

namespace Param_ItemNamespace.Services
{
    public interface ITrackingAPI
    {
       /// <summary>
        /// Track events
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        void TrackEvent(string eventName, IDictionary<string, string> properties);

        /// <summary>
        /// Tracks events 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <param name="measurement"></param>
        void TrackEvent(string eventName, IDictionary<string, string> properties, IDictionary<string, double> measurement);

        /// <summary>
        /// Track events
        /// </summary>
        /// <param name="eventName"></param>
        void TrackEvent(string eventName);

        /// <summary>
        /// Track Exceptions 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="eventProperties"></param>
        void TrackException(Exception exception, IDictionary<string, string> eventProperties);
    
        /// <summary>
        /// Tracks API calls and durations
        /// </summary>
        /// <param name="dependencyName"></param>
        /// <param name="commandName"></param>
        /// <param name="startTime"></param>
        /// <param name="duration"></param>
        /// <param name="success"></param>
        void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success);

        /// <summary>
        /// Tracks metric telemetry values
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        void TrackMetric(string name, double value, IDictionary<string, string> properties);

        /// <summary>
        /// Tracks page navigation
        /// </summary>
        /// <param name="name"></param>
        void TrackPageView(string name);
 
    }
}
