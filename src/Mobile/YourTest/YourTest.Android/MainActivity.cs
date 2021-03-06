using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Microsoft.Identity.Client;
using Prism;
using Prism.Ioc;
using YourTest.Azure;
using YourTest.Droid.Manager;
using YourTest.Manager;
using Autofac;
using YourTest.Auth;
using Xamarin.Android.Net;
using System.Net.Http;
using Prism.Autofac;
using CarouselView.FormsPlugin.Android;

namespace YourTest.Droid
{
    [Activity(
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
        )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var cb = containerRegistry.GetBuilder();

            cb.Register(c => new Authenticator(c.Resolve<AzureADAuthConfig>(), new UIParent(this)))
                .As<IAuthenticator>();

            cb.Register(c => new AndroidClientHandler())
                .Named<HttpMessageHandler>(App.IoCNameNativeHttpHandler);

            cb.Register(c => new IPAddressManager())
                .As<IIPAddressManager>();
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
            CarouselViewRenderer.Init();

            LoadApplication(new App(this));
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }

    }
}

