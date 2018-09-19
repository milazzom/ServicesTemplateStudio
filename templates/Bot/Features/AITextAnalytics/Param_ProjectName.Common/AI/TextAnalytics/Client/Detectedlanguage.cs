//-----------------------------------------------------------------------
// <copyright file="Detectedlanguage.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.Param_ItemNamespace.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class DetectedLanguage.
    /// </summary>
    [JsonObject]
    public class Detectedlanguage
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ISO 6391 Name.
        /// </summary>
        [JsonProperty(PropertyName = "iso6391Name")]
        public string IsoName { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public float Score { get; set; }
    }
}