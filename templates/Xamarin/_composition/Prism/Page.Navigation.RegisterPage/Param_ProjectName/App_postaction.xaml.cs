//{[{
using Param_RootNamespace.Views;
using Param_RootNamespace.ViewModels;
//}]}
namespace Param_RootNamespace
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            //^^
            //{[{
            containerRegistry.RegisterForNavigation<wts.ItemNamePage, wts.ItemNameViewModel >();
            //}]}
        }
    }
}
