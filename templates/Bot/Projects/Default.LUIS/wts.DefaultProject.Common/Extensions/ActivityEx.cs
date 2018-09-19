using System;
using System.Collections.Generic;
using Microsoft.Bot.Schema;

namespace Microsoft.Services.BotTemplates.Param_ProjectName.Common.Extensions
{
    public static class ActivityEx
    {
        public static void AddSuggestedActions(this Activity activity, List<string> actions)
        {
            if (activity.SuggestedActions == null)
            {
                activity.SuggestedActions = new SuggestedActions();
            }

            if (activity.SuggestedActions.Actions == null)
            {
                activity.SuggestedActions.Actions = new List<CardAction>();
            }

            foreach (var action in actions)
            {
                activity.SuggestedActions.Actions.Add(new CardAction(ActionTypes.ImBack,
                    action,
                    value: action.ToLower(),
                    displayText: action.ToLower(),
                    text: action.ToLower()));
            }
        }
    }
}