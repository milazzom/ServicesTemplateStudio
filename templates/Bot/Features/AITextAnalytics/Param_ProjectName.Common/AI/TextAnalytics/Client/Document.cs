//-----------------------------------------------------------------------
// <copyright file="Document.cs" company="Microsoft">
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.Param_ItemNamespace.Common.AI.TextAnalytics.Client
{
    /// <summary>
    /// Class Document.
    /// </summary>
    [JsonObject]
    public class Document
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}