using System.Collections.ObjectModel;
using Param_RootNamespace.Models;
using Param_RootNamespace.Views;
using Param_RootNamespace;
using Prism.Navigation;
using Prism.Commands;

namespace Param_RootNamespace.ViewModels
{
    public class RootMasterDetailPageViewModel : ViewModelBase
    {
        public RootMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigateCommand = new DelegateCommand(Navigate);
        }

        private MasterDetailPageMenuItem selectedMenuItem;
        public MasterDetailPageMenuItem SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }

        private async void Navigate()
        {
            await NavigationService.NavigateAsync($"NavigationPage/{SelectedMenuItem.TargetPage}");
        }

        public DelegateCommand NavigateCommand { get; private set; }

        public ObservableCollection<MasterDetailPageMenuItem> PrimaryMenuItems { get; private set; } = new ObservableCollection<MasterDetailPageMenuItem>
        {
        };
    }
}
