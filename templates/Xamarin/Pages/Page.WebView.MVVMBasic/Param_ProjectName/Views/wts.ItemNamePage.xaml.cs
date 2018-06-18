using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Param_ProjectName.Services;

using Param_ProjectName.ViewModels;

namespace Param_ProjectName.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wts.ItemNamePage : ContentPage
    {
        private wts.ItemNamePageViewModel ViewModel;
        public wts.ItemNamePage ()
        {
            InitializeComponent ();
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(On<Xamarin.Forms.PlatformConfiguration.iOS>(), true);
            ViewModel = new wts.ItemNamePageViewModel(_webView);
            ViewModel.Url = "https://developer.microsoft.com";
            ViewModel.GoCommand.Execute(null);
            BindingContext = ViewModel;
            BackButton.SetBinding(Button.CommandProperty, nameof(ViewModel.BackCommand));
            BackButton.SetBinding(Button.TextProperty, nameof(ViewModel.BackButtonText));
            ForwardButton.SetBinding(Button.CommandProperty, nameof(ViewModel.ForwardCommand));
            ForwardButton.SetBinding(Button.TextProperty, nameof(ViewModel.ForwardButtonText));
            UrlEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.Url));
            GoButton.SetBinding(Button.CommandProperty, nameof(ViewModel.GoCommand));
            GoButton.SetBinding(Button.TextProperty, nameof(ViewModel.GoButtonText));
        }


    }
}
