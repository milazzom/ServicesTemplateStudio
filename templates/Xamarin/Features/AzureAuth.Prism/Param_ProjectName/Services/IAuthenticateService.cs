using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Param_ItemNamespace.Services.Interfaces
{
    public enum AuthenticationResultCode
    {
        Success,
        ProviderError,
        Canceled,
        Denied,
        Unknown,
        None
    }

    public interface IAuthenticateService
    {
        string UserName { get; }

        AuthenticationResultCode TokenRequestStatus { get; }

        Task ClearTokenCacheAsync();

        Task<string> GetTokenAsync();
    }

    public interface IAuthenticatePlatformService
    {
        Task<Uri> GetRedirectURIAsync();
        IPlatformParameters PlatformParameters { get; }
        Task ClearCookies(string authority);
    }
}
