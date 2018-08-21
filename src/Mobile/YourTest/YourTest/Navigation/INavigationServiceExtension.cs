using System;
using Prism.Navigation;
using System.Threading.Tasks;
namespace YourTest.Navigation
{
    public static class INavigationServiceExtension
    {
        public static async Task NavigateAsync<TViewModel>(this INavigationService navigationService)
            where TViewModel : class
        {
            var viewModelType = typeof(TViewModel);
            await navigationService.NavigateAsync(viewModelType.Name).ConfigureAwait(false);
        }
    }
}
