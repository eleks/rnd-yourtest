using YourTest.REST;
using YourTest.Models;
using System.Windows.Input;
using Prism.Commands;
using System.Threading.Tasks;
using MvvmHelpers;
using YourTest.ViewModels.ActiveTest;
using YourTest.Auth;
using Prism.Navigation;
using YourTest.Navigation;

namespace YourTest.ViewModels
{
    public class TestsListViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Test> Source { get; } = new ObservableRangeCollection<Test>();

        public ICommand LoadCommand { get; }
        public ICommand SelectTestCommand { get; }
        public ICommand LogoutCommand { get; }

        public TestsListViewModel(
            ITestsRest testsRest
            , AuthSession authSession
            , INavigationService navigationService
            )
        {
            _testsRest = testsRest;
            _authSession = authSession;
            _navigationService = navigationService;

            LoadCommand = new DelegateCommand(async () => await LoadAsync());
            SelectTestCommand = new DelegateCommand<Test>(async (test) => await OnTestSelected(test));
            LogoutCommand = new DelegateCommand(async () => await LogOutAsync());
        }

        private async Task OnTestSelected(Test test)
        {
            var navParam = new NavigationParameters();
            navParam.Add("test", test);
            await _navigationService.NavigateAsync(nameof(ActiveTestPageViewModel), navParam);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCommand.Execute(null);
        }

        private async Task LoadAsync()
        {
            IsBusy = true;
            var testsData = await _testsRest.GetAllAsync();
            Source.ReplaceRange(testsData);
            IsBusy = false;
        }


        private async Task LogOutAsync()
        {
            try
            {
                await _authSession.SignOutAsync();
                await _navigationService.NavigateAsync<LoginViewModel>(true);
            }
            catch (Exception)
            { }
        }


        private readonly ITestsRest _testsRest;
        private readonly AuthSession _authSession;
        
        private readonly INavigationService _navigationService;
    }
}
