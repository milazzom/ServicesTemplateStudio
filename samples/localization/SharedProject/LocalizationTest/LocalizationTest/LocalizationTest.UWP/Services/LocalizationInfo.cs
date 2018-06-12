using System.Globalization;

using LocalizationTest.Services;

using Xamarin.Forms;

[assembly: Dependency(typeof(LocalizationTest.UWP.Services.LocalizationInfo))]
namespace LocalizationTest.UWP.Services
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
