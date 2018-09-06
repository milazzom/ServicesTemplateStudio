//-----------------------------------------------------------------------
// <copyright file="ResponseDocument.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class ResponseDocument.
    /// </summary>
    [JsonObject]
    public class ResponseDocument
    {
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public float Score { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}