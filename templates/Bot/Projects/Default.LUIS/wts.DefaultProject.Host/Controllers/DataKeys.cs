using System;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Host.Controllers
{
    /// <summary>
    /// Constants for keys used in ViewData and also for QueryString parameters (in some cases).
    /// </summary>
    public static class DataKeys
    {
        public const string UserId = "userId";
        public const string Fob = "fob";
        public const string Vin = "vin";
        public const string EmailAddress = "emailAddress";
        public const string WebChatSecret = "webChatSecret";
        public const string SpeechRecognizerSecret = "speechRecognizerSecret";
        public const string FirstName = "firstName";
        public const string LastName = "lastName";
    }
}