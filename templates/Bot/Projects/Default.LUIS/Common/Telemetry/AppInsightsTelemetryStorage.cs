using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.Telemetry
{
    /// <summary>
    /// A teletry storage provide for app Insights.
    /// </summary>
    public class AppInsightsTelemetryStorage : ITelemetryStorage
    {
        private readonly TelemetryClient _telemetryClient;

        public AppInsightsTelemetryStorage(TelemetryConfiguration configuration)
        {
            _telemetryClient = new TelemetryClient(configuration);
        }

        public async Task TrackEvent(string eventName, Dictionary<string, string> eventProperties = null, IDictionary<string, double> eventMetrics = null)
        {
            _telemetryClient.TrackEvent(eventName, eventProperties, eventMetrics);
        }

        public async Task TrackTrace(string message, SeverityLevel severityLevel = SeverityLevel.Information, IDictionary<string, string> properties = null)
        {
            _telemetryClient.TrackTrace(message, severityLevel, properties);
        }

        public async Task TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            _telemetryClient.TrackMetric(name, value, properties);
        }

        public async Task TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            _telemetryClient.TrackException(exception, properties, metrics);
        }
    }
}