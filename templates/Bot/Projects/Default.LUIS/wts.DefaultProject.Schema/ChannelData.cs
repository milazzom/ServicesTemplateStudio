using System;
using System.Collections.Generic;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Schema
{
    public class ChannelData
    {
        [JsonProperty(PropertyName = "vinNumber")]
        public string VinNumber { get; set; }

        [JsonProperty(PropertyName = "fobId")]
        public string FobId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "debug")]
        public bool Debug { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalJTokens { get; set; } = new Dictionary<string, JToken>();

        public static ChannelData GetChannelDataFromActivity(IActivity activity)
        {
            return activity.ChannelData != null ? JsonConvert.DeserializeObject<ChannelData>(activity.ChannelData?.ToString()) : null;
        }
    }
}