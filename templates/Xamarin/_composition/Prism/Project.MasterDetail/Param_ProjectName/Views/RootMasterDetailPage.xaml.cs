using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Param_RootNamespace.Models;

namespace Param_RootNamespace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootMasterDetailPage : Xamarin.Forms.MasterDetailPage
    {
        public RootMasterDetailPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }
    }
}
