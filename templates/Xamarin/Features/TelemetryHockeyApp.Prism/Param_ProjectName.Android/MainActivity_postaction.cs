using Prism.Ioc;
//^^
//{[{
using Param_RootNamespace.Services;
using Param_RootNamespace.Droid.Services;
using HockeyApp.Android;
//}]}

namespace Param_RootNamespace.Droid
{
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
//{[{        
        private readonly string HockeyAppId = string.Empty; // TODO - add Hockey App ID
        
//}]}
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
//{[{
            InitializeTelemetry();
//}]}
        }
//{[{

        private void InitializeTelemetry()
        {
            if (!string.IsNullOrEmpty(HockeyAppId))
            {
                HockeyApp.Android.CrashManager.Register(Application, HockeyAppId);
                HockeyApp.Android.Metrics.MetricsManager.Register(Application, HockeyAppId);
            }
        }
//}]}
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
//{[{
            container.Register(typeof(ITrackingAPI), typeof(TrackingAPI));
//}]}
        }
    }
}

