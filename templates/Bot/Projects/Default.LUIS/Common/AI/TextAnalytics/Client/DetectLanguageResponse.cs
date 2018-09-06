//-----------------------------------------------------------------------
// <copyright file="DetectLanguageResponse.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class DetectLanguageResponse.
    /// </summary>
    [JsonObject]
    public class DetectLanguageResponse
    {
        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        [JsonProperty(PropertyName = "documents")]
        public LanguageResponseDocument[] Documents { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public Error[] Errors { get; set; }
    }
}