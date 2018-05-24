using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Param_ProjectName.ViewModels
{
    class wts.ItemNameViewModel : BaseViewModel
    {
        public Command GoCommand { get; set; }
        public Command BackCommand { get; set; }
        public Command ForwardCommand { get; set; }
        private Xamarin.Forms.WebView _webView;

        public wts.ItemNameViewModel(Xamarin.Forms.WebView webView)
        {
            _webView = webView;
            _webView.Navigated += OnNavigated;
            GoCommand = new Xamarin.Forms.Command(GoButton_Clicked, () => { return !string.IsNullOrEmpty(Url); });
            BackCommand = new Command(BackButton_Clicked, () => CanGoBack);
            ForwardCommand = new Command(ForwardButton_Clicked, () => CanGoForward);
        }

        private void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            Url = e.Url;
            BackCommand?.ChangeCanExecute();
            ForwardCommand?.ChangeCanExecute();
        }

        public bool CanGoBack
        {
            get
            {
                return _webView.CanGoBack;
            }
        }

        public bool CanGoForward
        {
            get
            {
                return _webView.CanGoForward;
            }
        }

        private string urlString;
        public string Url
        {
            get => urlString;
            set
            {
                SetProperty(ref urlString, value);
            }
        }

        private void BackButton_Clicked()
        {
            _webView.GoBack();
            BackCommand?.ChangeCanExecute();
            ForwardCommand?.ChangeCanExecute();
        }

        private void ForwardButton_Clicked()
        {
            _webView.GoForward();
            BackCommand?.ChangeCanExecute();
            ForwardCommand?.ChangeCanExecute();

        }

        private void GoButton_Clicked()
        {
            _webView.Source = Url;
            BackCommand?.ChangeCanExecute();
            ForwardCommand?.ChangeCanExecute();
        }
    }
}
