﻿//using Param_RootNamespace.ViewModels.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Param_RootNamespace.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPageMaster : ContentPage
    {
        public ListView PrimaryListView;

        public MasterDetailPageMaster()
        {
            InitializeComponent();

            //BindingContext = new MasterDetailPageMasterViewModel();
            PrimaryListView = PrimaryMenuItemsListView;
        }
    }
}