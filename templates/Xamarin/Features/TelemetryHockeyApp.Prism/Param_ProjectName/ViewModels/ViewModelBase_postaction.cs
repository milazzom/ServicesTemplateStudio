using Prism.Navigation;
//{[{
using Xamarin.Forms;
using Param_RootNamespace.Services;
//}]}

namespace Param_RootNamespace.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
//{[{
        private readonly ITrackingService trackingService;
//}]}
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
//{[{
            this.trackingService = DependencyService.Get<ITrackingService>(DependencyFetchTarget.GlobalInstance); // Singleton is the default
//}]}
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
//{[{
            var pageName =  this.GetType().Name.Replace("ViewModel", string.Empty); // TODO - Figure out navigation enum for MasterDetail
            if (pageName != "RootMasterDetailPage")
            {
                trackingService.TrackEvent(string.Format("Navigated To: {0}", pageName));
            }
//}]}
        }
    }
}