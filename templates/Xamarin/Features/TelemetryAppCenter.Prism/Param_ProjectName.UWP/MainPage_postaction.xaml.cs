
using Prism.Ioc;
//^^
//{[{
using Param_RootNamespace.Services;
using Param_RootNamespace.UWP.Services;
//}]}

namespace Param_RootNamespace.UWP
{
    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
//{[{
            container.Register(typeof(ITrackingAPI), typeof(TrackingAPI));
//}]}
        }
    }
}