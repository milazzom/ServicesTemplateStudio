using System;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.ContentModerator
{
    /// <summary>
    /// Defines the options used by the content moderator service.
    /// </summary>
    public class ContentModeratorOptions
    {
        public string ServiceKey { get; set; }
        public string ServiceBaseUrl { get; set; }
        public bool AuthCorrect { get; set; }
        public bool DetectPii { get; set; }
        public bool Classify { get; set; }
    }
}