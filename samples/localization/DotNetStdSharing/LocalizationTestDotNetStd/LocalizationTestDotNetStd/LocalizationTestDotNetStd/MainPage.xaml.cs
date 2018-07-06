using LocalizationTestDotNetStd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalizationTestDotNetStd
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            GreetingLabel.Text = DependencyService.Get<ILocalizer>().GetStringForKey("Greeting");
		}
	}
}
