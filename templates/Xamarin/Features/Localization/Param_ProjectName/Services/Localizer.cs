using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(Param_ProjectName.Services.Localizer))]
namespace Param_ProjectName.Services
{
    public class Localizer : ILocalizer
    {
        private static string resourceName = GetResourceId();
        private static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(resourceName, IntrospectionExtensions.GetTypeInfo(typeof(Localizer)).Assembly));

        private static string GetResourceId()
        {
            string resourceName = null;
            switch(Device.RuntimePlatform)
            {
                case Device.Android:
                    resourceName = "Param_ProjectName.Android.Strings.AppResources";
                    break;
                case Device.iOS:
                    resourceName = "Param_ProjectName.iOS.AppResources";
                    break;
                case Device.UWP:
                    resourceName = "Param_ProjectName.UWP.Strings.AppResources";
                    break;
            }

            return resourceName;
        }
        public string GetStringForKey(string key)
        {
            string text = string.Empty;
            var currentCulture = DependencyService.Get<ILocalizationInfo>().GetCurrentCultureInfo();
            if(currentCulture == null)
            {
                currentCulture = CultureInfo.CurrentUICulture;
            }
            text = ResMgr.Value.GetString(key, currentCulture);
            return text;
        }
    }
}
