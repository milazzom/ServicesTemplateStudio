using Prism.Ioc;
//^^
//{[{
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Param_RootNamespace.Services.Interfaces;
using Param_RootNamespace.Droid;
//}]}

namespace Param_RootNamespace.Droid
{
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
//{[{

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
//}]}
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
//{[{
            container.Register(typeof(IAuthenticatePlatformService), typeof(AuthenticatePlatformService));
//}]}
        }
    }
}