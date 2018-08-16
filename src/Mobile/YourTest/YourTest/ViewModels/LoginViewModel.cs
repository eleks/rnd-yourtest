using System;
using System.Windows.Input;
using YourTest.Azure;
using Prism.Commands;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Diagnostics;
using YourTest.Auth;
using System.Net.Http;

namespace YourTest.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; }

        public LoginViewModel(
            AuthSession authSession,
            INavigationService navigationService,
            HttpClient client
            )
        {
            _authSession = authSession;
            _navigationService = navigationService;
            this.client = client;
            LoginCommand = new DelegateCommand(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            IsBusy = true;
            try
            {
                await _authSession.AuthenticateAsync();
                // todo await _navigationService.NavigateAsync(nameof(MainViewModel));
                var res = await client.GetAsync("https://roru-test-dev.azurewebsites.net/api/test");
                res.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private readonly AuthSession _authSession;
        private readonly INavigationService _navigationService;
        private readonly HttpClient client;
    }
}

