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
using YourTest.Navigation;
using Xamarin.Forms;
using Refit;
using YourTest.REST;
using YourTest.Manager;
using YourTest.ViewModels.ActiveTest;
using Plugin.DeviceInfo.Abstractions;
using Prism.Navigation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace YourTest
{
    public partial class App : PrismApplication
    {
        public static String IoCNameNativeHttpHandler { get; } = "NativeHandler";

        public App(IPlatformInitializer platformInitializer = default(IPlatformInitializer))
            : base(platformInitializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new ContentPage())
            {
                BarTextColor = Color.White,
            };
            await NavigationService.NavigateAsync<LoginViewModel>();

            try
            {
                await Container.Resolve<AuthSession>().AuthenticateSilentAsync();
                await NavigationService.NavigateAsync<TestsListViewModel>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterViews(containerRegistry);
            var builder = containerRegistry.GetBuilder();
            RegisterConfigs(builder);
            RegisterHttpHandlerStack(builder);
            RegisterRestServices(builder);
            RegisterManagers(builder);
            ConfigureViewModels(builder);
        }

        private void ConfigureViewModels(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var vm = new LoginViewModel(
                    c.Resolve<AuthSession>(),
                    c.ResolveNamed<INavigationService>(NavigationServiceName)
                )
                {
                    AppVersion = Plugin.DeviceInfo.CrossDeviceInfo.Current.AppVersion
                };
                return vm;
            })
            .AsSelf();
        }

        private static void RegisterRestServices(ContainerBuilder builder)
        {
            builder.Register(c => RestService.For<ITestsRest>(c.Resolve<HttpClient>()))
                .As<ITestsRest>();
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
                .Register(c => new HttpClient(c.Resolve<HttpMessageHandler>())
                {
                    BaseAddress = new Uri(Configuration.YourTest.RestEndpoint)
                })
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

        private static void RegisterManagers(ContainerBuilder builder)
        {
            builder.Register(c => new TcpHololensCommunicationManager())
                .As<IHololensCommunicationManager>();
        }

        private static void RegisterViews(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForViewModelNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForViewModelNavigation<TestsListPage, TestsListViewModel>();
            containerRegistry.RegisterForViewModelNavigation<ConnectHoloLensPage, ConnectHoloLensPageViewModel>();
            containerRegistry.RegisterForViewModelNavigation<ActiveTestPage, ActiveTestPageViewModel>();
            containerRegistry.RegisterForViewModelNavigation<TestSummeryPage, TestSummeryViewModel>();
        }
    }
}