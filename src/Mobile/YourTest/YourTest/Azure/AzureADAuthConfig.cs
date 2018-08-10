using System;
namespace YourTest.Azure
{
    public sealed class AzureADAuthConfig
    {
        public string Authority { get; set; }
        public string Resource { get; set; }
        public string ClientId { get; set; }
        public string ReturnUri { get; set; }
    }
}
