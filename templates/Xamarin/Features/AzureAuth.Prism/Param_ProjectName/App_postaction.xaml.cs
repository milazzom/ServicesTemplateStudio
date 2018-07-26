using Prism.Ioc;
//^^
//{[{
using Param_RootNamespace.Services;
using Param_RootNamespace.Services.Interfaces;
//}]}

namespace Param_RootNamespace
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
//^^
//{[{       
            containerRegistry.RegisterSingleton<IAuthenticateService, AuthenticateServiceADAL>();
//}]}
        }
    }
}