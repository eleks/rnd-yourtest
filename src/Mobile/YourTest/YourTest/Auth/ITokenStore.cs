using System;
namespace YourTest.Auth
{
    public interface ITokenStore
    {
        String Token { get; set; }
        Boolean HasToken { get; }
    }
}
