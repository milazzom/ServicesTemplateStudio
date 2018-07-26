using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Param_ItemNamespace.Services.Interfaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using Android.Webkit;

namespace Param_ItemNamespace.Droid
{
    public class AuthenticatePlatformService : IAuthenticatePlatformService
    {
        public IPlatformParameters PlatformParameters => new PlatformParameters((Android.App.Activity)Forms.Context);

        public async Task ClearCookies(string authority)
        {
            CookieManager.Instance.RemoveAllCookie();
            await Task.CompletedTask;
        }

        public Task<Uri> GetRedirectURIAsync()
        {
            //TODO: Get the URI from UWP App
            return Task.FromResult<Uri>(new Uri(""));
        }
    }
}
