using System;
namespace YourTest.Auth
{
    public class MemoryTokenStore : ITokenStore
    {
        public string Token { get; set; }

        public bool HasToken => !String.IsNullOrWhiteSpace(Token);
    }
}
