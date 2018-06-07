using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Param_ProjectName.ViewModels
{
    class WebViewPageTemplateViewModel : BaseViewModel
    {
        public Command GoCommand { get; set; }
        public Command BackCommand { get; set; }
        public Command ForwardCommand { get; set; }
        public Xamarin.Forms.WebView WebView {
            get
            {
                return _webView;
            }
            set {
                InternalViewUpdated(value);
            }
        }
        private Xamarin.Forms.WebView _webView;

        public WebViewPageTemplateViewModel()
        {
            GoCommand = new Xamarin.Forms.Command(GoButton_Clicked, () => { return !string.IsNullOrEmpty(Url); });
            BackCommand = new Command(BackButton_Clicked, () => CanGoBack);
            ForwardCommand = new Command(ForwardButton_Clicked, () => CanGoForward);
        }

        private void InternalViewUpdated(Xamarin.Forms.WebView webView)
        {
            if (_webView != null)
            {
                _webView.Navigated -= OnNavigated;
                _webView = null;
            }
            _webView = webView;
            _webView.Navigated += OnNavigated;
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
                return _webView != null
                ? _webView.CanGoBack
                : false;
            }
        }

        public bool CanGoForward
        {
            get
            {
                return _webView != null
                ? _webView.CanGoForward
                : false;
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
            _webView?.GoBack();
            BackCommand?.ChangeCanExecute();
            ForwardCommand?.ChangeCanExecute();
        }

        private void ForwardButton_Clicked()
        {
            _webView?.GoForward();
            BackCommand?.ChangeCanExecute();
            ForwardCommand?.ChangeCanExecute();
        }

        private void GoButton_Clicked()
        {
            if(_webView != null)
            {
                _webView.Source = Url;
            }

            BackCommand?.ChangeCanExecute();
            ForwardCommand?.ChangeCanExecute();
        }
    }
}
