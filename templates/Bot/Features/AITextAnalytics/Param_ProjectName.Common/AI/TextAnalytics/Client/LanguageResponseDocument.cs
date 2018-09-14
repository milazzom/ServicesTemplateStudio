//-----------------------------------------------------------------------
// <copyright file="LanguageResponseDocument.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class LanguageResponseDocument.
    /// </summary>
    [JsonObject]
    public class LanguageResponseDocument
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the detected languages.
        /// </summary>
        [JsonProperty(PropertyName = "detectedLanguages")]
        public Detectedlanguage[] DetectedLanguages { get; set; }
    }
}