using Prism.Ioc;
//^^
//{[{
using Param_RootNamespace.Services;
using Param_RootNamespace.iOS.Services;
using HockeyApp.iOS;
//}]}

namespace Param_RootNamespace.iOS
{
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
//{[{   
        private readonly string HockeyAppId = string.Empty; // TODO - add Hockey App ID
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
            var manager = BITHockeyManager.SharedHockeyManager;
            if (!string.IsNullOrEmpty(HockeyAppId))
            {
                manager.Configure(HockeyAppId);
                manager.StartManager();
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
