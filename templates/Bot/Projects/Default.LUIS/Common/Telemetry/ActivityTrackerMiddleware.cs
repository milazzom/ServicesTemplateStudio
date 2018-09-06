//-----------------------------------------------------------------------
// <copyright file="ActivityTrackerMiddleware.cs" company="Microsoft">
//     Copyright (c) 2017 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.Telemetry
{
    /// <summary>
    /// This middleware tracks incomming and outgoing activities.
    /// </summary>
    public class ActivityTrackerMiddleware : IMiddleware
    {
        private readonly ITelemetryLogger _telemetryLogger;

        public ActivityTrackerMiddleware(ITelemetryLogger telemetryLogger)
        {
            _telemetryLogger = telemetryLogger;
        }

        // We log each message in a consistent way within Application Insights along with LUIS intent information in order
        // to provide a consistent set of insights across all Bot engagements using a baseline PowerBI dashboard.
        // This middleware comoponent always passes on to the next in the pipeline
        public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
        {
            try
            {
                context.OnSendActivities(TrackOutgoingActivities);
                await _telemetryLogger.LogActivity(context.Activity, context);
            }
            catch (Exception ex)
            {
                await _telemetryLogger.LogException(ex, context);
            }

            await next().ConfigureAwait(false);
        }

        private async Task<ResourceResponse[]> TrackOutgoingActivities(ITurnContext context, List<Activity> activities, Func<Task<ResourceResponse[]>> next)
        {
            try
            {
                foreach (var activity in activities)
                {
                    await _telemetryLogger.LogActivity(activity, context);
                }
            }
            catch (Exception ex)
            {
                await _telemetryLogger.LogException(ex, context);
            }

            return await next().ConfigureAwait(false);
        }
    }
}