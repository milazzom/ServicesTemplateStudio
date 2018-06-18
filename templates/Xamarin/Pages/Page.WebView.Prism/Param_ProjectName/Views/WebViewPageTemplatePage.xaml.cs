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
    public partial class WebViewPageTemplatePage : ContentPage
    {
        private WebViewPageTemplateViewModel ViewModel;
        public WebViewPageTemplatePage ()
        {
            InitializeComponent ();
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(On<Xamarin.Forms.PlatformConfiguration.iOS>(), true);
            ViewModel = BindingContext as WebViewPageTemplateViewModel;
            ViewModel.WebView = _webView;
            ViewModel.Url = "https://developer.microsoft.com";
            ViewModel.GoCommand.Execute(null);
            BackButton.SetBinding(Button.CommandProperty, nameof(ViewModel.BackCommand));
            ForwardButton.SetBinding(Button.CommandProperty, nameof(ViewModel.ForwardCommand));
            UrlEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.Url));
            GoButton.SetBinding(Button.CommandProperty, nameof(ViewModel.GoCommand));
            GoButton.SetBinding(Button.TextProperty, nameof(ViewModel.GoButtonText));
            BackButton.SetBinding(Button.TextProperty, nameof(ViewModel.BackButtonText));
            ForwardButton.SetBinding(Button.TextProperty, nameof(ViewModel.ForwardButtonText));
        }
    }
}
