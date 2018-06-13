using Prism.Navigation;
//{[{
using Xamarin.Forms;
using Prism.Unity;
using Param_RootNamespace.Services;
//}]}

namespace Param_RootNamespace.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
//{[{

        protected ITrackingService TrackingService { get; private set; }
//}]}
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
//{[{
            var container = ((PrismApplication)(App.Current)).Container as Prism.Ioc.IContainerProvider;
            TrackingService = container.Resolve(typeof(ITrackingService)) as ITrackingService;
//}]}
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
//{[{
            var pageName =  this.GetType().Name.Replace("ViewModel", string.Empty); // TODO - Figure out navigation enum for MasterDetail
            if (pageName != "RootMasterDetailPage" && TrackingService != null)
            {
                TrackingService.TrackPageView(pageName);
            }
//}]}
        }
    }
}