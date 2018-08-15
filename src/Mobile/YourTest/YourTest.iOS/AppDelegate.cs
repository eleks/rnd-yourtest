using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Prism;
using Prism.Ioc;
using YourTest.Azure;
using Prism.Autofac;
using Autofac;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace YourTest.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var cb = containerRegistry.GetBuilder();

            cb.Register(c => new Authenticator(
                c.Resolve<AzureADAuthConfig>(),
                () => new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController/*, true, PromptBehavior.RefreshSession*/))
                ).As<IAuthenticator>();
        }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(this));

            return base.FinishedLaunching(app, options);
        }


    }
}
