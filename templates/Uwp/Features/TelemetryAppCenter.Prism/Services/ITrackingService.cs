using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Param_ItemNamespace.Services
{
    public enum TrackingLoggingLevel
    {
        Off = 0,
        Error = 1,
        Warning = 2,
        Information = 3,
        Verbose = 4
    }

    /// <summary>
    /// Tracks the application application usage.
    /// </summary>
    public interface ITrackingService
    {
        TrackingLoggingLevel AppLoggingLevel { get; set; }
        TrackingLoggingLevel TrackEventLogLevel { get; set; }
        TrackingLoggingLevel TrackDependencyLogLevel { get; set; }
        TrackingLoggingLevel TrackPageViewLogLevel { get; set; }
        TrackingLoggingLevel TrackExceptionLogLevel { get; set; }
        TrackingLoggingLevel TrackMetricLogLevel { get; set; }
        int AggregateMetricLoggingDelay { get; set; }

        /// <summary>
        /// Tracks custom events.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        void TrackEvent(string eventName);

        /// <summary>
        /// Tracks custom events.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="loggingLevel"></param>
        void TrackEvent(string eventName, TrackingLoggingLevel loggingLevel = TrackingLoggingLevel.Warning);

        /// <summary>
        /// Tracks a service call and tracks timing
        /// </summary>
        /// <param name="dependencyName"></param>
        /// <param name="commandName"></param>
        /// <param name="startTime"></param>
        /// <param name="duration"></param>
        /// <param name="success"></param>
        void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success);

        /// <summary>
        /// Tracks Exceptions
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="properties"></param>
        void TrackException(Exception exception, IDictionary<string, string> properties);

        /// <summary>
        /// Tracks a metric
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        void TrackMetric(string name, double value, IDictionary<string, string> properties);

        /// <summary>
        /// Tracks a metric of aggregated data for a specific timespan
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        /// <param name="duration"></param>
        /// <param name="loggingLevel"></param>
        void TrackAggregateMetric(string name, double value, IDictionary<string, string> properties, TimeSpan duration, TrackingLoggingLevel loggingLevel = TrackingLoggingLevel.Warning);

        /// <summary>
        /// Tracks PageViews
        /// </summary>
        /// <param name="name"></param>
        void TrackPageView(string name);
    }
}
