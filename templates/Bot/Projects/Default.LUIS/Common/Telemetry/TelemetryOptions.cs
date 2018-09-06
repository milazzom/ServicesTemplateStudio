using System;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.Telemetry
{
    /// <summary>
    /// Defines the options to be used for logging telemetry.
    /// </summary>
    public class TelemetryOptions
    {
        /// <summary>
        /// Create an instance of TelemetryOptions with the default values.
        /// </summary>
        public TelemetryOptions()
        {
            LogOriginalMessages = false;
            LogUserName = false;
            LogActivityInfo = false;
        }

        /// <summary>
        /// For some customers, logging the queries within Application Insights might be an so have provided a config setting to disable this feature
        /// </summary>
        public bool LogOriginalMessages { get; set; }

        /// <summary>
        /// Tells the logger if it should log a json with the serialized activity.
        /// </summary>
        /// <remarks>
        /// If true, the logger will serialize the complete activity as JSON, this is not recommended for production since the
        /// activity may contain sensitive data.
        /// Turn it on only during development to analyze the whole activity and add explicit logging only for the required
        /// properties. 
        /// </remarks>
        public bool LogActivityInfo { get; set; }

        /// <summary>
        /// For some customers, logging user name within Application Insights might be an issue so have provided a config setting to disable this feature
        /// </summary>
        public bool LogUserName { get; set; }

        /// <summary>
        /// Get or sets the BotId that is logging the event.
        /// </summary>
        public string BotId { get; set; }

        /// <summary>
        /// The storage provider used to persist telemetry.
        /// </summary>
        public ITelemetryStorage TelemetryStorageProvider { get; set; }
    }
}