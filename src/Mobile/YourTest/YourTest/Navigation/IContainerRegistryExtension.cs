using System;
using Prism.Ioc;
using Xamarin.Forms;
namespace YourTest.Navigation
{
    public static class IContainerRegistryExtension
    {
        public static void RegisterForViewModelNavigation<TView, TViewModel>(this IContainerRegistry containerRegistry)
            where TView : Page
            where TViewModel : class
        {
            var modelType = typeof(TViewModel);

            containerRegistry.RegisterForNavigation<TView, TViewModel>(modelType.Name);
        }
    }
}
