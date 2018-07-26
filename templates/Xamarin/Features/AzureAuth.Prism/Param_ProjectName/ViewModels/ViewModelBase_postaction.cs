using Prism.Navigation;
//{[{
using Xamarin.Forms;
using Prism.Unity;
using Param_RootNamespace.Services.Interfaces;
//}]}

namespace Param_RootNamespace.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
//{[{

        protected IAuthenticateService AuthenticateService { get; }
//}]}
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
//{[{
            var container = ((PrismApplication)(App.Current)).Container as Prism.Ioc.IContainerProvider;
            AuthenticateService = container.Resolve(typeof(IAuthenticateService)) as IAuthenticateService;
//}]}
        }
    }
}
