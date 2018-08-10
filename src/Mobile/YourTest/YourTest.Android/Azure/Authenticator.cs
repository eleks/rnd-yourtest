using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using YourTest.Azure;
using Plugin.CurrentActivity;

namespace YourTest.Droid.Azure
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(AzureADAuthConfig config)
        {
            string authority = config.Authority;
            string resource = config.Resource;
            string clientId = config.ClientId;
            string returnUri = config.ReturnUri;

            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
            {
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            }

            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(CrossCurrentActivity.Current.Activity);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
            return authResult;
        }
    }
}
