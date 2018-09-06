//-----------------------------------------------------------------------
// <copyright file="TextAnalyticsRequestDocument.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class TextAnalyticsRequestDocument.
    /// </summary>
    [JsonObject]
    public class TextAnalyticsRequestDocument
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}