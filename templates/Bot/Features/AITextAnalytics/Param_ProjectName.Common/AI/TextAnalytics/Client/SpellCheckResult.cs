//-----------------------------------------------------------------------
// <copyright file="SpellCheckResult.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class SpellCheckResult.
    /// </summary>
    [JsonObject]
    public class SpellCheckResult
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the flagged tokens.
        /// </summary>
        [JsonProperty(PropertyName = "flaggedTokens")]
        public SpellCheckToken[] FlaggedTokens { get; set; }
    }
}