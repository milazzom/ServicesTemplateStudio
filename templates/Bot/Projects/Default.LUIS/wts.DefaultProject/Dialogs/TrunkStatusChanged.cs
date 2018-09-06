using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Services.BotTemplates.wts.DefaultProject.Schema;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Dialogs
{
    public class TrunkStatusChanged :IDialog
    {
        public async Task DialogBegin(DialogContext dc, IDictionary<string, object> dialogArgs = null)
        {
            var channelData = ChannelData.GetChannelDataFromActivity(dc.Context.Activity);
            if (channelData.AdditionalJTokens.Keys.Contains("isTrunkOpen"))
            {
                var isTrunkOpen = channelData.AdditionalJTokens["isTrunkOpen"].ToObject<bool>();
                var trunkStatus = isTrunkOpen ? "opened" : "closed";
                await dc.Context.SendActivity($"Trunk is {trunkStatus}");
            }
        }
    }
}
