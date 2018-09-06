//-----------------------------------------------------------------------
// <copyright file="SpellCheckToken.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class SpellCheckToken.
    /// </summary>
    [JsonObject]
    public class SpellCheckToken
    {
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the flagged tokens.
        /// </summary>
        [JsonProperty(PropertyName = "suggestions")]
        public SpellCheckTokenSuggestion[] Suggestions { get; set; }
    }
}