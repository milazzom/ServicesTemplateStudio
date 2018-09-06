//-----------------------------------------------------------------------
// <copyright file="KeyPhraseResponse.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.LuisBot.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class KeyPhraseResponse.
    /// </summary>
    [JsonObject]
    public class KeyPhraseResponse
    {
        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        [JsonProperty(PropertyName = "documents")]
        public KeyPhraseDocument[] Documents { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public Error[] Errors { get; set; }
    }
}