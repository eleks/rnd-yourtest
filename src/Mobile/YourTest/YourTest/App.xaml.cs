using System;
using Autofac;
using Prism;
using Prism.Autofac;
using Prism.Ioc;
using Xamarin.Forms.Xaml;

using YourTest.Views;
using YourTest.ViewModels;
using System.Net.Http;
using YourTest.Http;
using YourTest.Auth;

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
            RegisterHttpHandlerStack(builder);
        }

        private void RegisterHttpHandlerStack(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .Register(c => new MemoryTokenStore())
                .As<ITokenStore>()
                .SingleInstance();

            containerBuilder.Register(c =>
            {
                HttpMessageHandler handler = c.ResolveNamed<HttpMessageHandler>(IoCNameNativeHttpHandler);

#if DEBUG
                // For debug purposes only
                // Must be in the bootom of real request
                handler = new LoggerHttpMessgeHandler(handler);
#endif
                // Provides request with valid token
                handler = new TokenAwareMeassageHandler(handler, c.Resolve<ITokenStore>());

                return handler;
            })
            .AsSelf();

            containerBuilder
                .Register(c => new HttpClient(c.Resolve<HttpMessageHandler>()))
                .AsSelf()
                .SingleInstance();
        }

        private static void RegisterConfigs(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(c =>
            {
                return new Azure.AzureADAuthConfig
                {
                    ClientId = Configuration.B2c.ClientId,
                    Authority = Configuration.B2c.Authority,
                    Scopes = new[] { Configuration.B2c.ApiScopes },
                    RedirectUri = Configuration.B2c.RedirectUri
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