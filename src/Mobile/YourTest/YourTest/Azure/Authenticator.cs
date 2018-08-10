using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace YourTest.Azure
{
    public class Authenticator : IAuthenticator
    {
        private readonly IPlatformParameters _platformParameters;

        public Authenticator() : this(null) { }
        public Authenticator(IPlatformParameters platformParameters) => _platformParameters = platformParameters;


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

            var platformParams = _platformParameters;

            var uri = new Uri(returnUri);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
            return authResult;
        }
    }
}
