using System;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.Telemetry
{
    /// <summary>
    /// Telemetry event names (logged in app insights under the name field).
    /// </summary>
    public class TelemetryEvents
    {
        /// <summary>
        /// The bot message received event.
        /// </summary>
        public const string BotMessageReceived = "BotMessageReceived";

        /// <summary>
        /// The bot message sent event.
        /// </summary>
        public const string BotMessageSent = "BotMessageSent";

        /// <summary>
        /// A LUIS result.
        /// </summary>
        public const string LuisResult = "BotLuisResult";

        /// <summary>
        /// The QnAMakerResult event.
        /// </summary>
        public const string QnAMakerResult = "QnAMakerResult";

        /// <summary>
        /// An event containing the result of a sentiment analysis.
        /// </summary>
        public const string SentimentAnalysisResult = "SentimentAnalysisResult";

        /// <summary>
        /// An event containing the key phrases in a user's utterance.
        /// </summary>
        public const string KeyPhrasesResult = "KeyPhrasesResult";

        /// <summary>
        /// An event containing the current status of a flow (started, done, canceled)
        /// </summary>
        public const string BotFlowStatus = "BotFlowStatus";
    }
}