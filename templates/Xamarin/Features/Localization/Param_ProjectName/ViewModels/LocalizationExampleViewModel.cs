using System.Globalization;


namespace Param_ItemNamespace.ViewModels
{
    public class LocalizationExampleViewModel: BaseViewModel
    {
        private Dictionary<string, CultureInfo> supportedCultures;

        public LocalizationExampleViewModel()
        {
            supportedCultures = new Dictionary<string, CultureInfo>()
            {
                { "English (United States)", new CultureInfo("en-US") },
                { "Spanish (Mexican)", new CultureInfo("es-MX") }
            };

        }
    }
}
