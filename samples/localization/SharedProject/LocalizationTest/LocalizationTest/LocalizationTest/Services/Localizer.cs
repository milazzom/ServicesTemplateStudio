using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

using Xamarin.Forms;

[assembly: Dependency(typeof(LocalizationTest.Services.Localizer))]
namespace LocalizationTest.Services
{
    public class Localizer : ILocalizer
    {
        
        private static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager($"{Assembly.GetExecutingAssembly().GetName().Name}.Strings.AppResources", IntrospectionExtensions.GetTypeInfo(typeof(Localizer)).Assembly));

        public string GetStringForKey(string key)
        {
            var resourceName = $"{Assembly.GetExecutingAssembly().GetName().Name}";
            string text = string.Empty;
            var currentCulture = DependencyService.Get<ILocalizationInfo>().GetCurrentCultureInfo();
            if (currentCulture == null)
            {
                currentCulture = CultureInfo.CurrentUICulture;
            }
            try
            {
                text = ResMgr.Value.GetString(key, currentCulture);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                text = key;
            }
            return text;
        }
    }
}
