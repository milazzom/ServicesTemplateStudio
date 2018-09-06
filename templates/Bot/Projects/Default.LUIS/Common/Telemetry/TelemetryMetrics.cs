using System;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.Telemetry
{
    /// <summary>
    /// Common property names for event metrics (logged in AppInsights under customMetrics).
    /// </summary>
    public class TelemetryMetrics
    {
        /// <summary>
        /// The Sentiment property.
        /// </summary>
        public const string SentimentScore = "SentimentScore";

        /// <summary>
        /// The IntentScore property.
        /// </summary>
        public const string IntentScoreProperty = "Score";

        /// <summary>
        /// The ConfidenceScore property.
        /// </summary>
        public const string ConfidenceScoreProperty = "ConfidenceScore";

        /// <summary>
        /// The confidence score for the top answer
        /// </summary>
        public const string QnAMakerTopAnswerScore = "QnAMakerTopAnswerScore";
    }
}