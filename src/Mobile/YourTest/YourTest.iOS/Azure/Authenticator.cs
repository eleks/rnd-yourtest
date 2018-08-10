using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using YourTest.Azure;

namespace YourTest.iOS.Azure
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
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(controller);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
            return authResult;
        }
    }
}
}
