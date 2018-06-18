using System.Globalization;
using Xamarin.Forms;
using Param_ProjectName.Services;

[assembly: Dependency(typeof(Param_ProjectName.UWP.Services.LocalizationInfo))]
namespace Param_ProjectName.UWP.Services
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
