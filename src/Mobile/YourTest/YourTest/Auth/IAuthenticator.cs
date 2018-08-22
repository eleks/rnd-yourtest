using System;
using System.Threading.Tasks;

namespace YourTest.Auth
{
    public interface IAuthenticator
    {
        Task<String> AuthenticateAsync();
        Task<String> AuthenticateSilentAsync();
    }
}
