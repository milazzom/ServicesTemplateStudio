using Prism.Ioc;
//^^
//{[{
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Param_RootNamespace.Services;
using Param_RootNamespace.iOS.Services;
//}]}

namespace Param_RootNamespace.iOS
{
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
//{[{   
        private readonly string AppCenterId = string.Empty; // TODO - add Hockey App ID
//}]}

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
//{[{           
            InitializeTelemetry();
//}]}
        }
//{[{
    
        private void InitializeTelemetry()
        {
            if (!string.IsNullOrEmpty(AppCenterId))
            {
                Microsoft.AppCenter.AppCenter.Start(AppCenterId, typeof(Analytics), typeof(Crashes));
            }
        }
//}]}
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
//{[{
            container.Register(typeof(ITrackingAPI), typeof(TrackingAPI));
//}]}
        }
    }
}
