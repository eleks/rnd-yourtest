﻿using System;
using System.Windows.Input;
using YourTest.Azure;
using Prism.Commands;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Diagnostics;
using YourTest.Auth;

namespace YourTest.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; }

        public LoginViewModel(
            AuthSession authSession,
            INavigationService navigationService
            )
        {
            _authSession = authSession;
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            IsBusy = true;
            try
            {
                await _authSession.AuthenticateAsync();
                // todo await _navigationService.NavigateAsync(nameof(MainViewModel));
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
    }
}

