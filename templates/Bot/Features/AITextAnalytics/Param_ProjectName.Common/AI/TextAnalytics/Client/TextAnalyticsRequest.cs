//-----------------------------------------------------------------------
// <copyright file="TextAnalyticsRequest.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.Param_ItemNamespace.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class TextAnalyticsRequest.
    /// </summary>
    [JsonObject]
    public class TextAnalyticsRequest
    {
        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        [JsonProperty(PropertyName = "documents")]
        public TextAnalyticsRequestDocument[] Documents { get; set; }
    }
}