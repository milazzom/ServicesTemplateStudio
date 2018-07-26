using Prism.Ioc;
//^^
//{[{
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Param_RootNamespace.Services.Interfaces;
using Param_RootNamespace.iOS;
//}]}

namespace Param_RootNamespace.iOS
{
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
//{[{
            container.Register(typeof(IAuthenticatePlatformService), typeof(AuthenticatePlatformService));
//}]}
        }
    }
}
