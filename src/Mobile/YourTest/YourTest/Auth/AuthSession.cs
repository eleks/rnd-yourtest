using System;
using System.Threading.Tasks;

namespace YourTest.Auth
{
    public class AuthSession
    {
        public AuthSession(IAuthenticator authenticator, ITokenStore tokenStore)
        {
            _authenticator = authenticator;
            _tokenStore = tokenStore;
        }

        public async Task AuthenticateAsync()
        {
            var res = await _authenticator.AuthenticateAsync().ConfigureAwait(false);
            _tokenStore.Token = res;
        }

        public async Task AuthenticateSilentAsync()
        {
            var res = await _authenticator.AuthenticateSilentAsync().ConfigureAwait(false);
            _tokenStore.Token = res;
        }


        private readonly IAuthenticator _authenticator;
        private readonly ITokenStore _tokenStore;
    }
}
