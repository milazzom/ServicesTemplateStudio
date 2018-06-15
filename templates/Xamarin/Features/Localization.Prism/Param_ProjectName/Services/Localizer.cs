using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Param_ProjectName.Services
{
    public class Localizer : ILocalizer
    {
        private const string resourceName = "Param_ProjectName.Strings.AppResources";
        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(resourceName, IntrospectionExtensions.GetTypeInfo(typeof(Localizer)).Assembly));
        private ILocalizationInfo localizationInfo;

        public Localizer(ILocalizationInfo localizationInfo)
        {
            this.localizationInfo = localizationInfo;
        }

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
