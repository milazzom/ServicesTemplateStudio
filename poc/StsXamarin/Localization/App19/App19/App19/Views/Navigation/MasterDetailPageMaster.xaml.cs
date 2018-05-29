using System.Collections.ObjectModel;

using App19.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App19.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPageMaster : ContentPage
    {
        public ListView PrimaryListView;

        public MasterDetailPageMaster()
        {
            InitializeComponent();

            PrimaryListView = PrimaryMenuItemsListView;
            PrimaryMenuItemsListView.ItemsSource = PrimaryMenuItems;
        }
            
        public ObservableCollection<MasterDetailPageMenuItem> PrimaryMenuItems { get; private set; } = new ObservableCollection<MasterDetailPageMenuItem>
        {
                new MasterDetailPageMenuItem { Title = "Main", TargetType = typeof(MainPage), IconSource = "blank.png"},
                new MasterDetailPageMenuItem { Title = "WebView", TargetType = typeof(WebViewPage), IconSource = "blank.png"},
        };
    }
}
