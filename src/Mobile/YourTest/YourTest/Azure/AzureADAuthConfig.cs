using System;
namespace YourTest.Azure
{
    public sealed class AzureADAuthConfig
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string[] Scopes { get; set; }
    }
}
