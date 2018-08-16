using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using YourTest.Auth;

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

        public Task<String> AuthenticateAsync()
        {
            var config = _config;
            IPlatformParameters platformParams = GetPlatformParameters();
            var tcs = new TaskCompletionSource<String>();

            AuthenticationContext authContext = GetAuthContextForAuthority(config.Authority);
            Task.Run(async () =>
            {
                try
                {
                    // Note: pass client id as resource parameter from answer https://stackoverflow.com/a/38374002/2198007
                    var authResult = await authContext.AcquireTokenAsync(
                        config.Resource
                        , config.ClientId
                        , new Uri(config.ReturnUri)
                        , platformParams
                        );

                    tcs.SetResult(authResult.AccessToken);
                }
                catch (AdalServiceException ex) when (ex.ErrorCode == AdalError.AuthenticationCanceled)
                {
                    tcs.SetCanceled();
                }
                catch (AdalException ex)
                {
                    tcs.SetException(ex);
                }

            });

            return tcs.Task;
        }

        public async Task<String> AuthenticateSilentAsync()
        {
            var config = _config;
            AuthenticationContext authContext = GetAuthContextForAuthority(config.Authority);

            try
            {
                var authResult = await authContext.AcquireTokenSilentAsync(
                    config.Resource
                    , config.ClientId
                );

                return authResult.AccessToken;
            }
            catch (AdalSilentTokenAcquisitionException)
            {
                return null;
            }
        }

        protected virtual IPlatformParameters GetPlatformParameters() => _platformParameters?.Invoke();

        private static AuthenticationContext GetAuthContextForAuthority(String authority)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
            {
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            }

            return authContext;
        }
    }
}
