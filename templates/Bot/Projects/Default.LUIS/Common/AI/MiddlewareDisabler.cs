using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.AI
{
    /// <summary>
    /// A delegate that can be used to prevent a middleware from running
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task<Boolean> MiddlewareDisabler(ITurnContext context);
}