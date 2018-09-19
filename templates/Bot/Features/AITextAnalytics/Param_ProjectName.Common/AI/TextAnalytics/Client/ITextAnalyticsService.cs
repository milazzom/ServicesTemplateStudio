using System;
using System.Threading.Tasks;

namespace Microsoft.Services.BotTemplates.Param_ItemNamespace.Common.AI.TextAnalytics.Client
{
    public interface ITextAnalyticsService
    {
        Task<string> GetKeyPhrases(string text);
        Task<SentimentResponse> GetSentiment(string text, string locale);
    }
}