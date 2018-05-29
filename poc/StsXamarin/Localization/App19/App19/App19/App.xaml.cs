using App19.Services;
using App19.Views;

using Xamarin.Forms;

namespace App19
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();
            MainPage = new Views.Navigation.MasterDetailPage();
            RegisterNavigationPages();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
        private void RegisterNavigationPages()
        {
            var navigationService = NavigationService.Instance;
            
            navigationService.Register("Main", typeof(MainPage));
            navigationService.Register("WebView", typeof(WebViewPage));
        }
    }
}
