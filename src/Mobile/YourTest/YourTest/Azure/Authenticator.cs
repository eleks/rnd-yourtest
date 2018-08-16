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
            _publicApp = new PublicClientApplication(config.ClientId);
            _publicApp.RedirectUri = config.ReturnUri;
        }

        public async Task<String> AuthenticateAsync()
        {
            var config = _config;
            UIParent uiparant = GetPlatformParameters();

            // Note: pass client id as resource parameter from answer https://stackoverflow.com/a/38374002/2198007
            var authResult = await _publicApp.AcquireTokenAsync(
            new string[]
            {
                "User.Read"
                //"https://roru-test-dev.azurewebsites.net/user_impersonation"
            },
            uiparant);



            return authResult.AccessToken;
        }


        public async Task<String> AuthenticateSilentAsync()
        {
            var config = _config;
            UIParent uiparant = GetPlatformParameters();

            // Note: pass client id as resource parameter from answer https://stackoverflow.com/a/38374002/2198007
            var authResult = await _publicApp.AcquireTokenSilentAsync(
            new string[]
            {
                "User.Read"
            },
            _publicApp.Users.FirstOrDefault());

            return authResult.AccessToken;
        }


        protected virtual UIParent GetPlatformParameters() => _uIParent;
    }
}
