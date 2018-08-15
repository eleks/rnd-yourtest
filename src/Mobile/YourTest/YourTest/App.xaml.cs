using System;
using Prism;
using Prism.Autofac;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using YourTest.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace YourTest
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer = default(IPlatformInitializer))
            : base(platformInitializer)
        {
            InitializeComponent();
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync(nameof(LoginPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterViews(containerRegistry);
        }

        private void RegisterViews(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
        }
    }
}