//-----------------------------------------------------------------------
// <copyright file="KeyPhraseDocument.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class KeyPhraseDocument.
    /// </summary>
    [JsonObject]
    public class KeyPhraseDocument
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the keyPhrases.
        /// </summary>
        [JsonProperty(PropertyName = "keyPhrases")]
        public string[] KeyPhrases { get; set; }
    }
}