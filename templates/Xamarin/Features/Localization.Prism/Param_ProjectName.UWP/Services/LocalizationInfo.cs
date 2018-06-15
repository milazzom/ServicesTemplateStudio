using System.Globalization;
using Param_ProjectName.Services;

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
