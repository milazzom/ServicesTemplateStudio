using Prism.Ioc;
//^^
//{[{
using Param_RootNamespace.Services;
using Param_RootNamespace.Android.Services;
//}]}

namespace Param_RootNamespace.Droid
{
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
//{[{
            container.Register(typeof(ILocalizationInfo), typeof(LocalizationInfo));
            container.Register(typeof(ILocalizer), typeof(Localizer));
//}]}
        }
    }
}
