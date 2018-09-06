using System;
using System.Threading.Tasks;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.AI.TextAnalytics.Client
{
    public interface ITextAnalyticsService
    {
        Task<string> GetKeyPhrases(string text);
        Task<SentimentResponse> GetSentiment(string text, string locale);
    }
}