using System.Globalization;
using Xamarin.Forms;
using App19.Services;

[assembly: Dependency(typeof(App19.UWP.Services.LocalizationInfo))]
namespace App19.UWP.Services
{
    class LocalizationInfo : ILocalizationInfo
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            return CultureInfo.CurrentUICulture;
        }

        public void SetLocale(CultureInfo ci)
        {
            CultureInfo.CurrentCulture = ci;
        }
    }
}
