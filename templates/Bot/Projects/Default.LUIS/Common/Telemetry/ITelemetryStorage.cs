using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DataContracts;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.Telemetry
{
    /// <summary>
    /// Defines the operations available to store telemtry. 
    /// </summary>
    public interface ITelemetryStorage
    {
        /// <summary>
        /// Logs an event.
        /// </summary>
        Task TrackEvent(string eventName, Dictionary<string, string> eventProperties = null, IDictionary<string, double> eventMetrics = null);

        /// <summary>
        /// Logs a trace.
        /// </summary>
        Task TrackTrace(string message, SeverityLevel severityLevel = SeverityLevel.Information, IDictionary<string, string> properties = null);

        /// <summary>
        /// Logs a metric
        /// </summary>
        Task TrackMetric(string name, double value, IDictionary<string, string> properties = null);

        /// <summary>
        /// Logs an exception
        /// </summary>
        Task TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
    }
}