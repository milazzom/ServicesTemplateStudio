using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Param_ItemNamespace.Services.Interfaces;
using Windows.Security.Authentication.Web;
using Windows.Web.Http;

namespace Param_ItemNamespace.UWP
{
    public class AuthenticatePlatformService : IAuthenticatePlatformService
    {
        public IPlatformParameters PlatformParameters => new PlatformParameters(PromptBehavior.Auto, false);

        public async Task ClearCookies(string authority)
        {
            Windows.Web.Http.Filters.HttpBaseProtocolFilter myFilter = new Windows.Web.Http.Filters.HttpBaseProtocolFilter();
            var cookieManager = myFilter.CookieManager;
            // Does not clear out cookies...
            HttpCookieCollection myCookieJar = cookieManager.GetCookies(new Uri(authority));
            foreach (HttpCookie cookie in myCookieJar)
            {
                cookieManager.DeleteCookie(cookie);
            }
            // Neither does this ...
            //var filter = new Windows.Web.Http.Filters.HttpBaseProtocolFilter();
            //filter.ClearAuthenticationCache();

            await Task.CompletedTask;
        }

        public Task<Uri> GetRedirectURIAsync()
        {
            var redirectUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            return Task.FromResult<Uri>(redirectUri);
        }
    }
}
