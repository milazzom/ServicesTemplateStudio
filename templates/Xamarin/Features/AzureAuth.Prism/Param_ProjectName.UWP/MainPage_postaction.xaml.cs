
using Prism.Ioc;
//^^
//{[{
using Param_RootNamespace.Services.Interfaces;
using Param_RootNamespace.UWP;
//}]}

namespace Param_RootNamespace.UWP
{
    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
//{[{
            container.Register(typeof(IAuthenticatePlatformService), typeof(AuthenticatePlatformService));
//}]}
        }
    }
}
