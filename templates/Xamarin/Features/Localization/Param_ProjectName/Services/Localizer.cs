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
    /// <summary>
    /// Class that resolves references to RESX files for use in application localization.
    /// NOTE:  When adding resource files within a shared project, you must manually perform the following steps:
    /// 1. Add the RESX file for the appropriate language to the Strings folder in the shared project.  Be sure to name it as follows: AppResources.culture.resx
    /// 2. Change the default build action on the RESX file to "Embedded Resource".
    /// 3. In each of the platform-specific .csproj files, add the following entry:
    ///       <ItemGroup>
    ///         <EmbeddedResource Update="Strings\AppResources.culture.resx" />
    ///       </ItemGroup >
    /// </summary>
    public class Localizer : ILocalizer
    {
        private static string resourceName = GetResourceId();
        private static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(resourceName, IntrospectionExtensions.GetTypeInfo(typeof(Localizer)).Assembly));

        /// <summary>
        /// Method to obtain the correct platform-specific resource ID for the AppResources file.
        /// </summary>
        /// <returns>Resource ID for AppResources.resx for the currently running platform.</returns>
        private static string GetResourceId()
        {
            string resourceName = null;
            switch(Device.RuntimePlatform)
            {
                case Device.Android:
                    resourceName = "Param_ProjectName.Droid.Strings.AppResources";
                    break;
                case Device.iOS:
                    resourceName = "Param_ProjectName.iOS.Strings.AppResources";
                    break;
                case Device.UWP:
                    resourceName = "Param_ProjectName.UWP.Strings.AppResources";
                    break;
            }

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
