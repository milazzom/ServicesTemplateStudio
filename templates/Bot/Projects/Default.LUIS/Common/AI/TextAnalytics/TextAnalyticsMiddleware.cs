using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics
{
    /// <summary>
    /// A middleware that uses the Text Analytics service to analyze the user's utterance (sentiment and key phrases)
    /// </summary>
    public class TextAnalyticsMiddleware : IMiddleware
    {
        /// <summary>
        /// The key used to store the sentiment results in the conversation state.
        /// </summary>
        public static readonly string SentimentResultKey = $"{typeof(TextAnalyticsMiddleware).FullName}.SentimentResultKey";

        /// <summary>
        /// The key used to store the sentiment score in the conversation state.
        /// </summary>
        public static readonly string SentimentScoreKey = $"{typeof(TextAnalyticsMiddleware).FullName}.SentimentScoreKey";

        private readonly TextAnalyticsOptions _textAnalyticsOptions;

        /// <summary>
        /// Instantiates 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="telemetryLogger"></param>
        public TextAnalyticsMiddleware(TextAnalyticsOptions options)
        {
            _textAnalyticsOptions = options;
        }

        /// <summary>
        /// Processes the incoming activity.
        /// </summary>
        public virtual async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
        {
            // Only log telemetry if we have a valid key and log sentiment is set to true
            if (!string.IsNullOrEmpty(_textAnalyticsOptions.TextAnalyticsKey) && !string.IsNullOrEmpty(context.Activity.Text))
            {
                var textAnalyticsService = new TextAnalyticsService(_textAnalyticsOptions.TextAnalyticsKey, _textAnalyticsOptions.TextAnalyticsEndpoint);
                if (_textAnalyticsOptions.AnalyzeSentiment)
                {
                    await AnalyzeSentiment(context, textAnalyticsService);
                }

                if (_textAnalyticsOptions.ExtractKeyPhrases)
                {
                    await ExtractKeyPhrases(context, textAnalyticsService);
                }
            }

            await next().ConfigureAwait(false);
        }

        private async Task AnalyzeSentiment(ITurnContext context, ITextAnalyticsService analyticsService)
        {
            //context.GetConversationState<StoreItem>()[SentimentScoreKey] = null;
            var state = ConversationState<Dictionary<string, object>>.Get(context);
            if (state.ContainsKey(SentimentScoreKey))
            {
                state.Remove(SentimentScoreKey);
            }

            // NOTE: before splitting, replace multiple instances of spaces with a single space 
            // and trim either end, so that we do not skew the amount of words in the trimmed list.
            var modifiedText = Regex.Replace(context.Activity.Text, @"\s+", " ").Trim();
            var words = modifiedText.Split(' ');

            if (words.Length < _textAnalyticsOptions.SentimentWordThreshold)
            {
                return;
            }

            //Now get the sentiment
            float? sentimentScore = null;
            var sentimentResponse = await analyticsService.GetSentiment(context.Activity.Text, context.Activity.Locale);

            //ConversationState<StoreItem>.Get(context)[SentimentResultKey] = sentimentResponse;
            state[SentimentResultKey] = sentimentResponse;

            if (sentimentResponse != null && (sentimentResponse.Errors == null || !sentimentResponse.Errors.Any()) && sentimentResponse.Documents != null && sentimentResponse.Documents.Any())
            {
                sentimentScore = sentimentResponse.Documents[0].Score;
            }

            //ConversationState<StoreItem>.Get(context)[SentimentScoreKey] = sentimentScore;
            state[SentimentScoreKey] = sentimentScore;
        }

        private async Task ExtractKeyPhrases(ITurnContext context, ITextAnalyticsService textAnalyticsService)
        {
            // Only perform key phrase extraction (makes for better analysis of utterances) if we have a valid key and key phrase extraction is set to true
            if (!_textAnalyticsOptions.ExtractKeyPhrases)
            {
                return;
            }

            var keyPhrases = await textAnalyticsService.GetKeyPhrases(context.Activity.Text);
        }

        public static bool IsLowSentiment(ITurnContext context, double sentimentThreshold)
        {
            var conversationState = context.GetConversationState<Dictionary<string, object>>();
            if (!conversationState.ContainsKey(TextAnalyticsMiddleware.SentimentScoreKey) || conversationState[TextAnalyticsMiddleware.SentimentScoreKey] == null)
            {
                return false;
            }

            var sentimetScore = Single.Parse(conversationState[TextAnalyticsMiddleware.SentimentScoreKey].ToString(), CultureInfo.InvariantCulture);
            return sentimetScore <= sentimentThreshold;
        }
    }
}