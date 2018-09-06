using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Ai.QnA;
using Microsoft.Bot.Schema;
using Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.Telemetry
{
    /// <summary>
    /// Defines the operations available to log telemtry for the bot.
    /// </summary>
    public interface ITelemetryLogger
    {
        Task LogActivity(string eventName, ITurnContext context, Activity activity, IDictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null);
        Task LogActivity(Activity activty, ITurnContext context, IDictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null);
        Task LogEvent(string eventName, ITurnContext context, IDictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null);
        Task LogException(Exception exception, ITurnContext context, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        Task LogMetric(string metricName, double metricValue, ITurnContext context, IDictionary<string, string> properties = null);
        Task LogTrace(string message, ITurnContext context, SeverityLevel severityLevel = SeverityLevel.Information, IDictionary<string, string> properties = null);
        Task LogQnAMakerResult(string question, string qnaMakerAppId, QueryResult[] results, ITurnContext context, Dictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null);
        Task LogLuisIntent(string luisAppId, ITurnContext context, Dictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null);
        Task LogSentimentAnalysisResult(float? sentimentScore, SentimentResponse sentimentResponse, ITurnContext context);
        Task LogKeyPhrasesResult(string keyPhrases, ITurnContext context);
    }
}