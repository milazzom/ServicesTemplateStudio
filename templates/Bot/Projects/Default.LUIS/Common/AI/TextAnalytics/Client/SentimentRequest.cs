//-----------------------------------------------------------------------
// <copyright file="SentimentRequest.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class SentimentRequest.
    /// </summary>
    [JsonObject]
    public class SentimentRequest
    {
        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        [JsonProperty(PropertyName = "documents")]
        public Document[] Documents { get; set; }
    }
}