//-----------------------------------------------------------------------
// <copyright file="TextAnalyticsService.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Helper class to wrap calls to the Text Analytics Cognitive Service
    /// </summary>
    public class TextAnalyticsService : ITextAnalyticsService
    {
        private const string _detectLanguageSuffix = "text/analytics/v2.0/languages";
        private const string _sentimentSuffix = "text/analytics/v2.0/sentiment";
        private const string _keyPhraseSuffix = "text/analytics/v2.0/keyPhrases";

        /// <summary>
        /// The OCP APIM subscription key header.
        /// </summary>
        private const string _apimSubscriptionKeyHeader = "Ocp-Apim-Subscription-Key";

        /// <summary>
        /// The application JSON content type.
        /// </summary>
        private const string _contentTypeJson = "application/json";

        private readonly TelemetryClient _telemetryClient = new TelemetryClient();
        private readonly string _textAnalyticsEndpoint;

        private readonly string _textAnalyticsKey;

        public TextAnalyticsService(string textAnalyticsKey, string textAnalyticsEndpoint)
        {
            _textAnalyticsKey = textAnalyticsKey;
            _textAnalyticsEndpoint = textAnalyticsEndpoint;

            if (string.IsNullOrEmpty(_textAnalyticsKey))
            {
                throw new ArgumentNullException("A value for 'TextAnalyticsKey' could not be found in application settings");
            }

            if (string.IsNullOrEmpty(_textAnalyticsEndpoint))
            {
                throw new ArgumentNullException("A value for 'textAnalyticsEndpoint' could not be found in application settings");
            }
        }

        /// <summary>
        /// Using the text analytics cognitive service, calculates the sentiments in the given text, also based on the given locale.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="locale">The text locale.</param>
        /// <returns>The sentiment response.</returns>
        public async Task<SentimentResponse> GetSentiment(string text, string locale)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            SentimentResponse sentimentResponse;
            try
            {
                var request = new SentimentRequest
                {
                    Documents = new[]
                    {
                        new Document
                        {
                            Id = Guid.NewGuid().ToString(),
                            Text = text,
                            Language = locale
                        }
                    }
                };
                using (var httpClient = new HttpClient())
                {
                    var response = await CallTextAnalyticsService(request, httpClient, _sentimentSuffix);
                    sentimentResponse = JsonConvert.DeserializeObject<SentimentResponse>(response);
                }

                // Make sure we have a valid response with no errors
                if (sentimentResponse?.Errors != null && sentimentResponse.Errors.Any())
                {
                    _telemetryClient.TrackTrace($"GetSentiment::Error from Text Analytics: ID:{sentimentResponse.Errors[0].Id}={sentimentResponse.Errors[0].Message}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and throw back to the calling method
                _telemetryClient.TrackException(ex);
                _telemetryClient.TrackTrace($"GetSentiment::Failed with exception: {ex.Message}");
                throw;
            }

            return sentimentResponse;
        }

        /// <summary>
        /// Uses the text analytics cognitive service to extract keywords from the given string.
        /// NOTE: This method requires that both the 'TextAnalyticsKey' and 'TextAnalyticsKeyPhraseEndpoint' are set in application settings.
        /// NOTE: This method returns the keywords separated by a space, so phrases are not preserved.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>The keywords separated by a space.</returns>
        public async Task<string> GetKeyPhrases(string text)
        {
            // Join each key phrase with a space
            var rawValue = await GetKeyPhrasesInternal(text);
            return rawValue != null ? string.Join(" ", rawValue) : null;
        }

        private async Task<string[]> GetKeyPhrasesInternal(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            string[] keyPhrases = null;
            try
            {
                // Create the text analytics request
                var request = new TextAnalyticsRequest
                {
                    Documents = new[]
                    {
                        new TextAnalyticsRequestDocument
                        {
                            Id = Guid.NewGuid().ToString(),
                            Text = text
                        }
                    }
                };

                using (var httpClient = new HttpClient())
                {
                    var response = await CallTextAnalyticsService(request, httpClient, _keyPhraseSuffix);
                    var keyPhraseResponse = JsonConvert.DeserializeObject<KeyPhraseResponse>(response);
                    if (keyPhraseResponse?.Documents != null &&
                        keyPhraseResponse.Documents.Any() &&
                        keyPhraseResponse.Documents[0]?.KeyPhrases != null &&
                        (keyPhraseResponse.Errors == null || !keyPhraseResponse.Errors.Any()))
                    {
                        // Only one document.
                        keyPhrases = keyPhraseResponse.Documents[0].KeyPhrases;
                    }
                    else if (keyPhraseResponse?.Errors != null && keyPhraseResponse.Errors.Any())
                    {
                        _telemetryClient.TrackTrace($"KeyPhraseExtraction::GetKeyPhrases:Error from Text Analytics: ID:{keyPhraseResponse.Errors[0].Id}={keyPhraseResponse.Errors[0].Message}");
                    }
                    else
                    {
                        _telemetryClient.TrackTrace("KeyPhraseExtraction::GetKeyPhrases:Unknown error from Text Analytics");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and throw back to the calling method
                _telemetryClient.TrackException(ex);
                _telemetryClient.TrackTrace($"KeyPhraseExtraction::GetKeyPhrases failed with exception: {ex.Message}");
                throw;
            }

            return keyPhrases;
        }

        private async Task<string> CallTextAnalyticsService(object request, HttpClient httpClient, string serviceSuffix)
        {
            httpClient.DefaultRequestHeaders.Add(_apimSubscriptionKeyHeader, _textAnalyticsKey);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentTypeJson));
            httpClient.BaseAddress = new Uri(_textAnalyticsEndpoint);

            // Serialize the object and we use byte[] to avoid html encoding issues around >, < and &
            var requestBody = JsonConvert.SerializeObject(request);
            var byteData = Encoding.UTF8.GetBytes(requestBody);
            using (var baData = new ByteArrayContent(byteData))
            {
                baData.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync(serviceSuffix, baData);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Text Analytics failed. " + content);
                }

                // Attempt to deserialize the object into a sentiment response
                return content;
            }
        }
    }
}