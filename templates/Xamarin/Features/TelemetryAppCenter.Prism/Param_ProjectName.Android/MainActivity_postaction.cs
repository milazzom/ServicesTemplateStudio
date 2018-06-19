using Prism.Ioc;
//^^
//{[{
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Param_RootNamespace.Services;
using Param_RootNamespace.Droid.Services;
//}]}

namespace Param_RootNamespace.Droid
{
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
//{[{        
       private readonly string AppCenterId = string.Empty; // TODO - add Hockey App ID
        
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
            if (!string.IsNullOrEmpty(AppCenterId))
            {
                Microsoft.AppCenter.AppCenter.Start(AppCenterId, typeof(Analytics), typeof(Crashes));
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