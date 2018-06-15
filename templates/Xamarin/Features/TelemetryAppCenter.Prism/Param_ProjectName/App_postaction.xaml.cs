using Prism.Ioc;
//^^
//{[{
using Param_RootNamespace.Services;
//}]}

namespace Param_RootNamespace
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
//^^
//{[{       
            containerRegistry.RegisterSingleton<ITrackingService, TrackingService>();
//}]}
        }
    }
}