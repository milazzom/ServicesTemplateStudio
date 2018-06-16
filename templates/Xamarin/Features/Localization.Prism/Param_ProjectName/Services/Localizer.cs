using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Param_ProjectName.Services
{
    /// <summary>
    /// Class that resolves references to RESX files for use in application localization.
    /// </summary>
    public class Localizer : ILocalizer
    {
        private static string resourceName = GetResourceId();
        private static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(resourceName, IntrospectionExtensions.GetTypeInfo(typeof(Localizer)).Assembly));
        private ILocalizationInfo localizationInfo;

        public Localizer(ILocalizationInfo localizationInfo)
        {
            this.localizationInfo = localizationInfo;
        }

        /// <summary>
        /// Method to obtain the correct platform-specific resource ID for the AppResources file.
        /// </summary>
        /// <returns>Resource ID for AppResources.resx for the currently running platform.</returns>
        private static string GetResourceId()
        {
            return resourceName;
        }

        /// <summary>
        /// Method to obtain a string from the AppResources RESX file associated with the current culture.
        /// </summary>
        /// <param name="key">Name of the key</param>
        /// <returns>String value corresponding to the specified key</returns>s
        public string GetStringForKey(string key)
        {
            string text = string.Empty;
            var currentCulture = localizationInfo.GetCurrentCultureInfo();
            if(currentCulture == null)
            {
                currentCulture = CultureInfo.CurrentUICulture;
            }
            text = ResMgr.Value.GetString(key, currentCulture);
            return text;
        }
    }
}
