using System;
using Prism.Navigation;
using System.Threading.Tasks;
namespace YourTest.Navigation
{
    public static class INavigationServiceExtension
    {
        public static async Task NavigateAsync<TViewModel>(this INavigationService navigationService, Boolean closeCurrent = false)
            where TViewModel : class
        {
            var viewModelType = typeof(TViewModel);
            var path = closeCurrent
                ? $"../{viewModelType.Name}"
                : viewModelType.Name;

            await navigationService.NavigateAsync(path).ConfigureAwait(false);
        }
    }
}
