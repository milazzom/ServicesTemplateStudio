using System;
using Xamarin.Forms;
using LocalizationTest.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace LocalizationTest
{
	public partial class App : Application
	{
		
		public App ()
		{
			InitializeComponent();


			MainPage = new LocalizationTestPage();
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
	}
}
