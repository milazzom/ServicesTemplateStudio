//-----------------------------------------------------------------------
// <copyright file="TelemetryLogger.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Ai.LUIS;
using Microsoft.Bot.Builder.Ai.QnA;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;
using Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client;
using Newtonsoft.Json;
using Validation;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.Telemetry
{
    /// <summary>
    /// Simple helper for creating telemetry in a consistent way for a dashboard
    /// </summary>
    public class TelemetryLogger : ITelemetryLogger
    {
        /// <summary>
        /// The client info identifier in the Entities collection of the activity.
        /// </summary>
        /// <remarks>
        /// Skype and MSTeams send a clientinfo entity that includes platform, locale, conuntry.
        /// WebChat doesn't send any client info.
        /// Cortana doesn't send client Info, it sends a lot of other entities.
        /// </remarks>
        private const string _clientInfoEnittyIdentifier = "clientInfo";

        private readonly TelemetryOptions _telemetryOptions;

        /// <summary>
        /// Helper class used to log telemetry about the bot. 
        /// </summary>
        /// <param name="telemetryOptions"></param>
        public TelemetryLogger(TelemetryOptions telemetryOptions)
        {
            Requires.NotNull(telemetryOptions, nameof(telemetryOptions));
            _telemetryOptions = telemetryOptions;
        }

        /// <summary>
        /// Logs an activity.
        /// </summary>
        /// <param name="activity">The activity to be logged</param>
        /// <param name="context">The turn context for hte activity</param>
        /// <param name="additionalProperties">An optional dictionary with additional string properties to be logged</param>
        /// <param name="additionalMetrics">An optional dictionary with additional values to be logged</param>
        /// <remarks>
        /// The eventName is infered from the properties in the activity.
        /// If the ReplyToId property is null, then is incoming event (BotMessageReceived),
        /// If it is not null, then is an outgoing activity (BotMessageSent).
        /// </remarks>
        public virtual async Task LogActivity(Activity activity, ITurnContext context, IDictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null)
        {
            var eventName = activity.ReplyToId == null ? TelemetryEvents.BotMessageReceived : TelemetryEvents.BotMessageSent;
            await LogActivity(eventName, context, activity, additionalProperties, additionalMetrics);
        }

        /// <summary>
        /// Logs an Activity with a custom event name.
        /// </summary>
        /// <param name="eventName">The name of the event that will be logged.</param>
        /// <param name="context">The turn context for hte activity</param>
        /// <param name="activity">The activity to be logged</param>
        /// <param name="additionalProperties">An optional dictionary with additional string properties to be logged</param>
        /// <param name="additionalMetrics">An optional dictionary with additional values to be logged</param>
        public virtual async Task LogActivity(string eventName, ITurnContext context, Activity activity, IDictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null)
        {
            // Track the event with an exception wrapper
            await TrackEventWithExceptionWrapperAsync(async tc =>
            {
                var eventProperties = new Dictionary<string, string>();
                var eventMetrics = new Dictionary<string, double>();

                AppendPropertiesFromActivity(activity, eventProperties);

                // Append/Update extra properites if present.
                AppendOrUpdateWithExtraProperties(eventProperties, additionalProperties);

                // Append/update extra metrics if present
                AppendOrUpdateWithExtraMetrics(eventMetrics, additionalMetrics);

                await tc.TrackEvent(eventName, eventProperties, eventMetrics);
            });
        }

        /// <summary>
        /// Log a custom event with the given name, properties and metrics.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="context">The turn context in which the event happened</param>
        /// <param name="additionalProperties">An optional dictionary with additional string properties to be logged</param>
        /// <param name="additionalMetrics">An optional dictionary with additional values to be logged</param>
        public virtual async Task LogEvent(string eventName, ITurnContext context, IDictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null)
        {
            Requires.NotNullOrEmpty(eventName, nameof(eventName));
            await LogActivity(eventName, context, context.Activity, additionalProperties, additionalMetrics);
        }

        /// <summary>
        /// Send a trace message for display in Diagnostic Search.
        /// This acts as a simple wrapper around the TelemetryClient.TrackTrace method.
        /// </summary>
        /// <param name="message">The trace string.</param>
        /// <param name="context"></param>
        /// <param name="severityLevel">The severity level.</param>
        /// <param name="properties">The properties.</param>
        public virtual async Task LogTrace(string message, ITurnContext context, SeverityLevel severityLevel = SeverityLevel.Information, IDictionary<string, string> properties = null)
        {
            Requires.NotNullOrEmpty(message, nameof(message));

            // Track the event with an exception wrapper
            await TrackEventWithExceptionWrapperAsync(async tc =>
            {
                var traceProperties = new Dictionary<string, string>();
                AppendPropertiesFromActivity(context.Activity, traceProperties);
                AppendOrUpdateWithExtraProperties(traceProperties, properties);
                await tc.TrackTrace(message, severityLevel, traceProperties);
            });
        }

        /// <summary>
        /// Send a metric message for display in Diagnostic Search.
        /// This acts as a simple wrapper around the TelemetryClient.TrackMetric method.
        /// </summary>
        /// <param name="metricName">The metric name.</param>
        /// <param name="metricValue">The value for metric.</param>
        /// <param name="context">The ITurnContext.</param>
        /// <param name="properties">The properties.</param>
        public virtual async Task LogMetric(string metricName, double metricValue, ITurnContext context, IDictionary<string, string> properties = null)
        {
            await TrackEventWithExceptionWrapperAsync(async tc =>
            {
                var metricProperties = new Dictionary<string, string>();
                AppendPropertiesFromActivity(context.Activity, metricProperties);
                AppendOrUpdateWithExtraProperties(metricProperties, properties);
                await tc.TrackMetric(metricName, metricValue, metricProperties);
            });
        }

        /// <summary>
        /// Send a track exception message for display in Diagnostic Search.
        /// This acts as a simple wrapper around the TelemetryClient.TrackException method.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="context"></param>
        /// <param name="properties">The properties.</param>
        /// <param name="metrics">The metrics.</param>
        public virtual async Task LogException(Exception exception, ITurnContext context, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            Requires.NotNull(exception, nameof(exception));

            // Track the event with an exception wrapper
            await TrackEventWithExceptionWrapperAsync(async tc =>
            {
                var prop = new Dictionary<string, string>();
                AppendPropertiesFromActivity(context.Activity, prop);
                AppendOrUpdateWithExtraProperties(prop, properties);
                await tc.TrackException(exception, prop, metrics);
            });
        }

        /// <summary>
        /// Logs an event that a Luis Intent has been identified.
        /// Logs an event called "LuisIntent.{TopScoringIntent.Intent}".
        /// </summary>
        public virtual async Task LogLuisIntent(string luisAppId, ITurnContext context, Dictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null)
        {
            // Simple guard statements
            Requires.NotNull(context, nameof(context));

            // Track the event with an exception wrapper
            await TrackEventWithExceptionWrapperAsync(async tc =>
            {
                var luisRecognizerResult = context.Services.Get<RecognizerResult>(LuisRecognizerMiddleware.LuisRecognizerResultKey);
                if (luisRecognizerResult == null)
                {
                    return;
                }

                //TODO Improve Entity support 
                var topScoringIntent = luisRecognizerResult.GetTopScoringIntent();
                var telemetryProperties = new Dictionary<string, string>
                {
                    {TelemetryProperties.IntentProperty, topScoringIntent.intent},
                    {TelemetryProperties.LuisAppId, luisAppId},
                    {TelemetryProperties.IntentInfo, JsonConvert.SerializeObject(topScoringIntent.intent)}
                };

                var telemetryMetrics = new Dictionary<string, double>
                {
                    {TelemetryMetrics.IntentScoreProperty, topScoringIntent.score}
                };

                AppendPropertiesFromActivity(context.Activity, telemetryProperties);
                AppendOrUpdateWithExtraProperties(telemetryProperties, additionalProperties);
                AppendOrUpdateWithExtraMetrics(telemetryMetrics, additionalMetrics);

                // Track the event
                await tc.TrackEvent(TelemetryEvents.LuisResult, telemetryProperties, telemetryMetrics);
            });
        }

        /// <summary>
        /// Logs the results of a sentiment analysis request. 
        /// </summary>
        public virtual async Task LogSentimentAnalysisResult(float? sentimentScore, SentimentResponse sentimentResponse, ITurnContext context)
        {
            Dictionary<string, double> eventMetrics = null;
            if (sentimentScore != null)
            {
                eventMetrics = new Dictionary<string, double>
                {
                    {TelemetryMetrics.SentimentScore, sentimentScore.Value}
                };
            }

            var eventProperties = new Dictionary<string, string>
            {
                {TelemetryProperties.SentimentAnalysisResult, JsonConvert.SerializeObject(sentimentResponse)}
            };
            await LogEvent(TelemetryEvents.SentimentAnalysisResult, context, eventProperties, eventMetrics);
        }

        /// <summary>
        /// Logs key phrases in the user utterance
        /// </summary>
        public virtual async Task LogKeyPhrasesResult(string keyPhrases, ITurnContext context)
        {
            // TODO: check if privacy settings apply here.
            var eventProperties = new Dictionary<string, string>
            {
                {TelemetryProperties.KeyPhrases, keyPhrases}
            };
            await LogEvent(TelemetryEvents.KeyPhrasesResult, context, eventProperties);
        }

        /// <summary>
        /// Logs results of QnAMaker query.
        /// Logs an event called "QnAMakerResponse".
        /// </summary>
        /// <param name="question"></param>
        /// <param name="qnaMakerAppId"></param>
        /// <param name="results"></param>
        /// <param name="context"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="additionalMetrics"></param>
        public virtual async Task LogQnAMakerResult(string question, string qnaMakerAppId, QueryResult[] results, ITurnContext context, Dictionary<string, string> additionalProperties = null, IDictionary<string, double> additionalMetrics = null)
        {
            // Simple guard statements
            Requires.NotNull(context, nameof(context));
            Requires.NotNullOrEmpty(question, nameof(question));

            // Track the event with an exception wrapper
            await TrackEventWithExceptionWrapperAsync(async tc =>
            {
                var telemetryProperties = new Dictionary<string, string>
                {
                    {TelemetryProperties.QnAMakerAppId, qnaMakerAppId},
                    {TelemetryProperties.QnAMakerQuestion, question},
                    {TelemetryProperties.QnAMakerTopQuestion, results?.Length > 0 ? results[0].Questions[0] : null},
                    {TelemetryProperties.QnAMakerTopAnswer, results?.Length > 0 ? results[0].Answer : null},
                    {TelemetryProperties.QnAMakerAllQuestions, results?.Length > 0 ? JsonConvert.SerializeObject(results[0].Questions) : null},
                    {TelemetryProperties.QnAMakerAllAnswers, results?.Length > 0 ? JsonConvert.SerializeObject(results) : null}
                };

                var telemetryMetrics = new Dictionary<string, double>
                {
                    {TelemetryMetrics.QnAMakerTopAnswerScore, results?.Length > 0 ? results[0].Score : 0}
                };

                AppendPropertiesFromActivity(context.Activity, telemetryProperties);
                AppendOrUpdateWithExtraProperties(telemetryProperties, additionalProperties);
                AppendOrUpdateWithExtraMetrics(telemetryMetrics, additionalMetrics);

                // Track the event
                await tc.TrackEvent(TelemetryEvents.QnAMakerResult, telemetryProperties, telemetryMetrics);
            });
        }

        /// <summary>
        /// Helper method to create common properties for an Activity
        /// </summary>
        private void AppendPropertiesFromActivity(Activity activity, Dictionary<string, string> telemetryProperties)
        {
            // General metadata...
            telemetryProperties.Add(TelemetryProperties.BotId, _telemetryOptions.BotId);

            if (!string.IsNullOrEmpty(activity.Id))
            {
                telemetryProperties.Add(TelemetryProperties.ActivityId, activity.Id);
            }

            if (!string.IsNullOrEmpty(activity.Type))
            {
                telemetryProperties.Add(TelemetryProperties.ActivityType, activity.Type);
            }

            if (!string.IsNullOrEmpty(activity.Name))
            {
                telemetryProperties.Add(TelemetryProperties.Name, activity.Name);
            }

            if (_telemetryOptions.LogActivityInfo)
            {
                telemetryProperties.Add(TelemetryProperties.ActivityInfo, JsonConvert.SerializeObject(activity));
            }

            if (!string.IsNullOrEmpty(activity.ChannelId))
            {
                telemetryProperties.Add(TelemetryProperties.ChannelId, activity.ChannelId);
            }

            if (activity.From != null)
            {
                telemetryProperties.Add(TelemetryProperties.FromId, activity.From.Id);
                if (_telemetryOptions.LogUserName)
                {
                    telemetryProperties.Add(TelemetryProperties.FromName, activity.From.Name);
                }
            }

            if (activity.Conversation != null)
            {
                telemetryProperties.Add(TelemetryProperties.ConversationId, activity.Conversation.Id);
                telemetryProperties.Add(TelemetryProperties.ConversationName, activity.Conversation.Name);
            }

            if (!string.IsNullOrEmpty(activity.Locale))
            {
                telemetryProperties.Add(TelemetryProperties.Locale, activity.Locale);
                telemetryProperties.Add(TelemetryProperties.Language, CultureInfo.GetCultureInfo(activity.Locale).EnglishName);
            }

            if (activity.Recipient != null)
            {
                telemetryProperties.Add(TelemetryProperties.RecipientId, activity.Recipient.Id);
                telemetryProperties.Add(TelemetryProperties.RecipientName, activity.Recipient.Name);
            }

            if (!string.IsNullOrEmpty(activity.ReplyToId))
            {
                telemetryProperties.Add(TelemetryProperties.ReplyToId, activity.ReplyToId);
            }

            // For some customers, logging the queries within Application Insights might be an so have provided a config setting to disable this feature
            if (_telemetryOptions.LogOriginalMessages)
            {
                if (!string.IsNullOrEmpty(activity.Text))
                {
                    telemetryProperties.Add(TelemetryProperties.Text, activity.Text);
                }

                if (!string.IsNullOrEmpty(activity.Speak))
                {
                    telemetryProperties.Add(TelemetryProperties.Speak, activity.Speak);
                }

                if (activity.Value != null)
                {
                    telemetryProperties.Add(TelemetryProperties.Value, JsonConvert.SerializeObject(activity.Value));
                }
            }

            if (!string.IsNullOrEmpty(activity.ChannelData?.ToString()))
            {
                telemetryProperties.Add(TelemetryProperties.ChannelData, JsonConvert.SerializeObject(activity.ChannelData));
            }

            var clientInfoEntity = activity.Entities?.FirstOrDefault(e => e.Type == _clientInfoEnittyIdentifier);
            if (clientInfoEntity != null)
            {
                telemetryProperties.Add(TelemetryProperties.ClientInfo, JsonConvert.SerializeObject(clientInfoEntity));
            }

            if (activity.Properties?.Count > 0)
            {
                telemetryProperties.Add(TelemetryProperties.ActivityProperties, JsonConvert.SerializeObject(activity.Properties));
            }
        }

        private static void AppendOrUpdateWithExtraMetrics(IDictionary<string, double> eventMetrics, IDictionary<string, double> metrics)
        {
            if (metrics == null)
            {
                return;
            }

            foreach (var valuePair in metrics)
            {
                if (eventMetrics.ContainsKey(valuePair.Key))
                {
                    eventMetrics[valuePair.Key] = valuePair.Value;
                }
                else
                {
                    eventMetrics.Add(valuePair.Key, valuePair.Value);
                }
            }
        }

        private static void AppendOrUpdateWithExtraProperties(IDictionary<string, string> eventProperties, IDictionary<string, string> additionalProperties)
        {
            if (additionalProperties == null)
            {
                return;
            }

            foreach (var valuePair in additionalProperties)
            {
                if (eventProperties.ContainsKey(valuePair.Key))
                {
                    eventProperties[valuePair.Key] = valuePair.Value;
                }
                else
                {
                    eventProperties.Add(valuePair.Key, valuePair.Value);
                }
            }
        }

        /// <summary>
        /// Tracks an event with the given exception wrapper (the code to track an event should be in the action passed through).
        /// This ensures that we 'swallow' any exceptions that may interfere with the bot.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        private async Task TrackEventWithExceptionWrapperAsync(Func<ITelemetryStorage, Task> action)
        {
            try
            {
                await action(_telemetryOptions.TelemetryStorageProvider);
            }
            catch (Exception ex)
            {
                try
                {
                    // catch all general exceptions as we do not want to crash the bot instance
                    await _telemetryOptions.TelemetryStorageProvider.TrackException(ex);
                }
                catch
                {
                    // Do nothing if the track exception also fails
                }
            }
        }
    }
}