using System;
using Autofac;
using Prism;
using Prism.Autofac;
using Prism.Ioc;
using Xamarin.Forms.Xaml;

using YourTest.Views;
using YourTest.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace YourTest
{
    public partial class App : PrismApplication
    {
        public static String IoCNameNativeHttpHandler { get; } = "NativeHandler";

        public App(IPlatformInitializer platformInitializer = default(IPlatformInitializer))
            : base(platformInitializer)
        {
            InitializeComponent();
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync(nameof(LoginViewModel));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterViews(containerRegistry);
            var builder = containerRegistry.GetBuilder();
            RegisterConfigs(builder);
        }

        private static void RegisterConfigs(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(c =>
            {
                return new Azure.AzureADAuthConfig
                {
                    ClientId = Configuration.AADClientId,
                    Authority = Configuration.AADAuthority,
                    Resource = Configuration.AADResource,
                    ReturnUri = Configuration.AADRedirectUri
                };
            }).AsSelf();
        }

        private static void RegisterViews(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>(nameof(LoginViewModel));
            containerRegistry.RegisterForNavigation<MainPage>();
        }
    }
}