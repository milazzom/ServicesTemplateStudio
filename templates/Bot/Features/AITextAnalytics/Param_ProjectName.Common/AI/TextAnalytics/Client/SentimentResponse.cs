//-----------------------------------------------------------------------
// <copyright file="SentimentResponse.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.Param_ItemNamespace.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class SentimentResponse.
    /// </summary>
    [JsonObject]
    public class SentimentResponse
    {
        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        [JsonProperty(PropertyName = "documents")]
        public ResponseDocument[] Documents { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public Error[] Errors { get; set; }
    }
}