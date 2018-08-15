using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace YourTest.Azure
{
    public class Authenticator : IAuthenticator
    {
        private readonly AzureADAuthConfig _config;
        private readonly Func<IPlatformParameters> _platformParameters;

        public Authenticator(AzureADAuthConfig config) : this(config, null) { }
        public Authenticator(AzureADAuthConfig config, Func<IPlatformParameters> platformParametersProvider)
        {
            _config = config;
            _platformParameters = platformParametersProvider;
        }

        public async Task<AuthenticationResult> AuthenticateAsync()
        {
            var config = _config;

            string authority = config.Authority;
            string resource = config.Resource;
            string clientId = config.ClientId;
            string returnUri = config.ReturnUri;

            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
            {
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            }

            var platformParams = _platformParameters?.Invoke();

            // Note: pass client id as resource parameter from answer https://stackoverflow.com/a/38374002/2198007
            var authResult = await authContext.AcquireTokenAsync(
                resource
                , clientId
                , new Uri(returnUri)
                , platformParams
                );

            return authResult;
        }
    }
}
