using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Ai.LUIS;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Services.BotTemplates.LuisBot.Common.AI;
using Validation;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.Telemetry
{
    /// <summary>
    /// This middleware tracks LUIS results.
    /// </summary>
    public class LuisTrackerMiddleware : IMiddleware
    {
        private readonly LinkedList<MiddlewareDisabler> _intentDisablers = new LinkedList<MiddlewareDisabler>();

        private readonly string _luisAppId;
        private readonly ITelemetryLogger _telemetryLogger;

        public LuisTrackerMiddleware(ITelemetryLogger telemetryLogger, string luisAppId)
        {
            _telemetryLogger = telemetryLogger;
            _luisAppId = luisAppId;
        }

        public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
        {
            var isEnabled = await IsMiddlewareEnabled(context).ConfigureAwait(false);
            if (isEnabled && context.Services.Get<RecognizerResult>(LuisRecognizerMiddleware.LuisRecognizerResultKey) != null)
            {
                await _telemetryLogger.LogLuisIntent(_luisAppId, context);
            }

            await next().ConfigureAwait(false);
        }

        public LuisTrackerMiddleware OnEnabled(MiddlewareDisabler preCondition)
        {
            Requires.NotNull(preCondition, nameof(preCondition));

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
    }
}