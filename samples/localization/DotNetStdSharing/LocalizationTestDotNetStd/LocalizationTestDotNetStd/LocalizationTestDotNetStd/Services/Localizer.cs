using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalizationTestDotNetStd.Services.Localizer))]
namespace LocalizationTestDotNetStd.Services
{
    public class Localizer : ILocalizer
    {
        private const string resourceName = "LocalizationTestDotNetStd.Strings.AppResources";
        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(resourceName, IntrospectionExtensions.GetTypeInfo(typeof(Localizer)).Assembly));


        public string GetStringForKey(string key)
        {
            string text = string.Empty;
            var currentCulture = DependencyService.Get<ILocalizationInfo>().GetCurrentCultureInfo();
            if (currentCulture == null)
            {
                currentCulture = CultureInfo.CurrentUICulture;
            }
            text = ResMgr.Value.GetString(key, currentCulture);
            return text;
        }
    }
}
