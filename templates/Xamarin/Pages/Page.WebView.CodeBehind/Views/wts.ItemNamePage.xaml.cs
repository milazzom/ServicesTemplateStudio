using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Param_ProjectName.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class wts.ItemNamePage : ContentPage
	{

        public wts.ItemNamePage ()
		{
			InitializeComponent ();
            UrlEntry.Text = "https://developer.microsoft.com";
            _webView.Source = new UrlWebViewSource()
            {
                Url = UrlEntry.Text
            };
            UpdateButtonStates();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GoButton.Clicked += GoButton_Clicked;
            BackButton.Clicked += BackButton_Clicked;
            ForwardButton.Clicked += ForwardButton_Clicked;
            _webView.Navigated += OnNavigated;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GoButton.Clicked -= GoButton_Clicked;
            BackButton.Clicked -= BackButton_Clicked;
            ForwardButton.Clicked -= ForwardButton_Clicked;
            _webView.Navigated -= OnNavigated;
        }

        private void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            UrlEntry.Text = e.Url;
        }

        private void ForwardButton_Clicked(object sender, EventArgs e)
        {
            if(_webView.CanGoForward)
            {
                _webView.GoForward();
            }
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            ForwardButton.IsEnabled = _webView.CanGoForward;
            BackButton.IsEnabled = _webView.CanGoBack;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            if(_webView.CanGoBack)
            {
                _webView.GoBack();
            }
            UpdateButtonStates();
        }

        private void GoButton_Clicked(object sender, EventArgs e)
        {
            var url = UrlEntry.Text;
            if(!string.IsNullOrEmpty(url))
            {
                _webView.Source = new UrlWebViewSource()
                {
                    Url = url
                };
                UpdateButtonStates();
            }
        }
    }
}
