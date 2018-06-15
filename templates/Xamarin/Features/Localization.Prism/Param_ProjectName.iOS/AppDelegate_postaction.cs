using Prism.Ioc;
//^^
//{[{
using Param_RootNamespace.Services;
using Param_RootNamespace.iOS.Services;
//}]}

namespace Param_RootNamespace.iOS
{

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
//{[{
            container.Register(typeof(ILocalizationInfo), typeof(LocalizationInfo));
            container.Register(typeof(ILocalizer), typeof(Localizer));
//}]}
        }
    }
}
