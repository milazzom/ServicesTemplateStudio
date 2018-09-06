using System;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.AI.TextAnalytics
{
    /// <summary>
    /// Defines the options to be used for calling the text analytics service.
    /// </summary>
    public class TextAnalyticsOptions
    {
        public TextAnalyticsOptions()
        {
            SentimentWordThreshold = 3;
            ExtractKeyPhrases = true;
            AnalyzeSentiment = true;
        }

        /// <summary>
        /// Indicates if the telemetry helper should attempt to extract key phrases from the requestand log them.
        /// </summary>
        public bool ExtractKeyPhrases { get; set; }

        public bool AnalyzeSentiment { get; set; }

        /// <summary>
        /// The sentiment word threshold. This value indicates the minimum number of words in a query
        /// before attempting to analyze sentiment.
        /// </summary>
        public int SentimentWordThreshold { get; set; }

        public string TextAnalyticsEndpoint { get; set; }

        public string TextAnalyticsKey { get; set; }
    }
}