//-----------------------------------------------------------------------
// <copyright file="SpellCheckTokenSuggestion.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class SpellCheckTokenSuggestion.
    /// </summary>
    [JsonObject]
    public class SpellCheckTokenSuggestion
    {
        /// <summary>
        /// Gets or sets the suggestion.
        /// </summary>
        [JsonProperty(PropertyName = "suggestion")]
        public string Suggestion { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }
    }
}