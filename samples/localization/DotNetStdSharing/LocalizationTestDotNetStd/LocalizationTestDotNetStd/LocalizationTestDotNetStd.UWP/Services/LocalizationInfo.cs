using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using LocalizationTestDotNetStd.Services;
using System.Globalization;

[assembly: Dependency(typeof(LocalizationTestDotNetStd.UWP.Services.LocalizationInfo))]
namespace LocalizationTestDotNetStd.UWP.Services
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
