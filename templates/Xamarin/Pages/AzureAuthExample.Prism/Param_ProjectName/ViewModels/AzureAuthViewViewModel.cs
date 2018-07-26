using Prism.Commands;
using Prism.Navigation;
using System;
using Param_ItemNamespace.Services.Interfaces;

namespace Param_ItemNamespace.ViewModels
{
	public class AzureAuthViewViewModel : ViewModelBase
    {
        public DelegateCommand LoginCommand { get; }
        public DelegateCommand LogoutCommand { get; }

        public AzureAuthViewViewModel(INavigationService navigationService) : base(navigationService)
        {
            LoginCommand = new DelegateCommand(OnLoginCommand);
            LogoutCommand = new DelegateCommand(OnLogoutCommand);
        }

        private async void OnLogoutCommand()
        {
            await AuthenticateService.ClearTokenCacheAsync();
        }

        private async void OnLoginCommand()
        {
            var token = await AuthenticateService.GetTokenAsync();
            if (AuthenticateService.TokenRequestStatus == AuthenticationResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert($"Token {AuthenticateService.UserName}", token, "Okay", "Cancel");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No Token", Enum.GetName(typeof(AuthenticationResultCode), AuthenticateService.TokenRequestStatus), "Cancel");
            }
        }
    }
}