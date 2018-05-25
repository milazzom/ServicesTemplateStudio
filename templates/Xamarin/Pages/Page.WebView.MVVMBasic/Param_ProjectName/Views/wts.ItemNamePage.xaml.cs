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
            ForwardButton.SetBinding(Button.CommandProperty, nameof(ViewModel.ForwardCommand));
            UrlEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.Url));
            GoButton.SetBinding(Button.CommandProperty, nameof(ViewModel.GoCommand));
            GoButton.Text = DependencyService.Get<ILocalizer>().GetStringForKey("wts.ItemNameGoButton");
            BackButton.Text = DependencyService.Get<ILocalizer>().GetStringForKey("wts.ItemNameBackButton");
            ForwardButton.Text = DependencyService.Get<ILocalizer>().GetStringForKey("wts.ItemNameForwardButton");
		}


	}
}
