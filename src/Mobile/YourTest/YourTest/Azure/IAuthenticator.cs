using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace YourTest.Azure
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> AuthenticateAsync();
    }
}
