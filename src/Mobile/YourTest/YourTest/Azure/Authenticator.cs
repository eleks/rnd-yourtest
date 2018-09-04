using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using YourTest.Auth;

namespace YourTest.Azure
{
    public class Authenticator : IAuthenticator
    {
        private readonly AzureADAuthConfig _config;
        private readonly UIParent _uIParent;
        private readonly PublicClientApplication _publicApp;

        public Authenticator(AzureADAuthConfig config) : this(config, null) { }
        public Authenticator(AzureADAuthConfig config, UIParent uIParent)
        {
            _config = config;
            _uIParent = uIParent;
            _publicApp = new PublicClientApplication(
                config.ClientId,
                config.Authority)
            {
                RedirectUri = config.RedirectUri
            };
        }

        public async Task<String> AuthenticateAsync()
        {
            var config = _config;
            UIParent uiparant = GetPlatformParameters();

            var authResult = await _publicApp.AcquireTokenAsync(config.Scopes, uiparant);

            return authResult.AccessToken;
        }


        public async Task<String> AuthenticateSilentAsync()
        {
            var config = _config;

            var authResult = await _publicApp.AcquireTokenSilentAsync(
            config.Scopes,
            _publicApp.Users.FirstOrDefault());

            return authResult.AccessToken;
        }

        public Task SignOut()
        {
            foreach (var u in _publicApp.Users)
            {
                _publicApp.Remove(u);
            }

            return Task.FromResult(true);
        }


        protected virtual UIParent GetPlatformParameters() => _uIParent;
    }
}
