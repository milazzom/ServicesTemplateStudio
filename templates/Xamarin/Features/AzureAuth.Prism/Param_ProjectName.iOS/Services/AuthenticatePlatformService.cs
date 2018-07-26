using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Param_ItemNamespace.Services.Interfaces;
using UIKit;

namespace Param_ItemNamespace.iOS
{
    public class AuthenticatePlatformService : IAuthenticatePlatformService
    {
        public IPlatformParameters PlatformParameters => new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController);

        public async Task ClearCookies(string authority)
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
            {
                CookieStorage.DeleteCookie(cookie);
            }
            await Task.CompletedTask;
        }

        public Task<Uri> GetRedirectURIAsync()
        {
            //TODO: Get the URI from UWP App
            return Task.FromResult<Uri>(new Uri(""));
        }
    }
}
