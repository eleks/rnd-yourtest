using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace YourTest.Auth
{
    public interface IAuthenticator
    {
        Task<String> AuthenticateAsync();
        Task<String> AuthenticateSilentAsync();
    }
}
