using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using YourTest.Auth;

using Header = System.Net.Http.Headers.AuthenticationHeaderValue;

namespace YourTest.Http
{
    public class TokenAwareMessageHandler : DelegatingHandler
    {
        private readonly ITokenStore _tokenStore;

        public TokenAwareMessageHandler(
            HttpMessageHandler innerHandler,
            ITokenStore tokenStore)
            : base(innerHandler)
        {
            _tokenStore = tokenStore;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_tokenStore.HasToken)
            {
                request.Headers.Authorization = new Header("Bearer", _tokenStore.Token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
