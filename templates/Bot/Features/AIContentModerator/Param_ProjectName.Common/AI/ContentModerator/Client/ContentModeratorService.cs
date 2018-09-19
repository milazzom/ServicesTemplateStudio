using System;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.CognitiveServices.ContentModerator;
using Microsoft.CognitiveServices.ContentModerator.Models;

namespace Microsoft.Services.BotTemplates.Param_ItemNamespace.Common.AI.ContentModerator.Client
{
    /// <summary>
    /// Provides access to the Content Moderator Service API.
    /// For more info, see: 
    /// https://docs.microsoft.com/en-us/azure/cognitive-services/content-moderator/overview
    /// </summary>
    public class ContentModeratorService : IDisposable
    {
        private readonly ContentModeratorClient _client;

        public ContentModeratorService(string key, string baseUrl)
        {
            _client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(key))
            {
                BaseUrl = baseUrl
            };
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

        public async Task<Screen> ScreenText(string phrase, string isoLanguage = null, bool autocorrect = false, bool detectPii = false, bool classify = true)
        {
            if (string.IsNullOrEmpty(isoLanguage))
            {
                isoLanguage = (await _client.TextModeration.DetectLanguageAsync("text/plain", phrase)).DetectedLanguageProperty;
            }

            return await _client.TextModeration.ScreenTextAsync(isoLanguage, "text/plain", phrase, autocorrect, detectPii, null, classify);
            //var lang = await CMClient.TextModeration.DetectLanguageAsync("text/plain", caption);
            //var oRes = await CMClient.TextModeration.ScreenTextWithHttpMessagesAsync(lang.DetectedLanguageProperty, "text/plain", caption, null, null, null, true);
            //response = oRes.Response;
            //responseContent = await response.Content.ReadAsStringAsync();
            //retry = false;
        }
    }
}