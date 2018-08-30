using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace YourTest.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await AttractUser();
        }

        private async Task AttractUser()
        {
            await Task.Delay(800);
            await _buttonLogin.ScaleTo(1.3, 400, easing: Easing.SinInOut);
            await _buttonLogin.ScaleTo(1, 400, easing: Easing.SinInOut);
        }
    }
}
