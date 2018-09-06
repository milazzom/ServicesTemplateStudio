using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.CognitiveServices.ContentModerator.Models;
using Microsoft.Services.BotTemplates.LuisBot.Common.AI.ContentModerator.Client;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.AI.ContentModerator
{
    public class ContentModeratorMiddleware : IMiddleware
    {
        /// <summary>
        /// The key used to store the sentiment score in the conversation state.
        /// </summary>
        public const string ContentModerationStateKey = "Microsoft.Services.BotHelpers.Core.ContentModerator.ContentModeratorMiddleware";

        private readonly ContentModeratorOptions _contentModeratorOptions;
        private readonly LinkedList<MiddlewareDisabler> _intentDisablers = new LinkedList<MiddlewareDisabler>();

        public ContentModeratorMiddleware(ContentModeratorOptions options)
        {
            _contentModeratorOptions = options;
        }

        public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
        {
            //context.GetConversationState<StoreItem>()[ContentModerationStateKey] = null;
            var state = ConversationState<Dictionary<string, object>>.Get(context);
            if (state.ContainsKey(ContentModerationStateKey))
            {
                state.Remove(ContentModerationStateKey);
            }

            var isEnabled = await IsMiddlewareEnabled(context).ConfigureAwait(false);
            var message = context.Activity.AsMessageActivity()?.Text;
            if (isEnabled && !string.IsNullOrEmpty(message))
            {
                using (var contentModeratorService = new ContentModeratorService(_contentModeratorOptions.ServiceKey, _contentModeratorOptions.ServiceBaseUrl))
                {
                    var locale = context.Activity.AsMessageActivity().Locale;
                    locale = string.IsNullOrEmpty(locale) ? CultureInfo.CurrentCulture.ThreeLetterISOLanguageName : new CultureInfo(locale).ThreeLetterISOLanguageName;
                    var result = await contentModeratorService.ScreenText(message, locale, _contentModeratorOptions.AuthCorrect, _contentModeratorOptions.DetectPii, _contentModeratorOptions.Classify);
                    if (result != null)
                    {
                        //context.GetConversationState<StoreItem>()[ContentModerationStateKey] = result;
                        state[ContentModerationStateKey] = result;
                    }
                }
            }

            await next().ConfigureAwait(false);
        }

        public ContentModeratorMiddleware OnEnabled(MiddlewareDisabler preCondition)
        {
            if (preCondition == null)
            {
                throw new ArgumentNullException(nameof(preCondition));
            }

            _intentDisablers.AddLast(preCondition);

            return this;
        }

        private async Task<bool> IsMiddlewareEnabled(ITurnContext context)
        {
            foreach (var userCode in _intentDisablers)
            {
                var isEnabled = await userCode(context).ConfigureAwait(false);
                if (isEnabled == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CurseWordsDetected(ITurnContext context)
        {
            var conversationState = context.GetConversationState<Dictionary<string, object>>();
            if (!conversationState.ContainsKey(ContentModerationStateKey) || conversationState[ContentModerationStateKey] == null)
            {
                return false;
            }

            var screenResults = (Screen)conversationState[ContentModerationStateKey];
            var termCount = screenResults.Terms != null ? screenResults.Terms?.Count : 0;
            return termCount > 0;
        }
    }
}