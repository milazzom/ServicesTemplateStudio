using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Param_ItemNamespace.ViewModels;

namespace Param_ItemNamespace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalizationExamplePage : ContentPage
    {
        public LocalizationExamplePage ()
        {
            InitializeComponent ();
            BindingContext = new LocalizationExampleViewModel();
            GreetingLabel.SetBinding(Label.TextProperty, nameof(LocalizationExampleViewModel.Greeting));

        }
    }
}
