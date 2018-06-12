using LocalizationTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalizationTest.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocalizationTestPage : ContentPage
	{
		public LocalizationTestPage ()
		{
			InitializeComponent ();
            LocalizedGreeting.Text = DependencyService.Get<ILocalizer>().GetStringForKey("LocalizedGreeting");
		}
	}
}