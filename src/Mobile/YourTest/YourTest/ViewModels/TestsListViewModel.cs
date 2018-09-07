using YourTest.REST;
using YourTest.Models;
using System.Windows.Input;
using Prism.Commands;
using System.Threading.Tasks;
using MvvmHelpers;
using Prism.Navigation;
using YourTest.ViewModels.ActiveTest;

namespace YourTest.ViewModels
{
    public class TestsListViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Test> Source { get; } = new ObservableRangeCollection<Test>();

        public ICommand LoadCommand { get; }
        public ICommand SelectTestCommand { get; }


        public TestsListViewModel(ITestsRest testsRest,
                                  INavigationService navigationService)
        {
            _testsRest = testsRest;
            _navigationService = navigationService;

            LoadCommand = new DelegateCommand(async () => await LoadAsync());
            SelectTestCommand = new DelegateCommand<Test>(async (test) => await OnTestSelected(test));

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


        private readonly ITestsRest _testsRest;
        private readonly INavigationService _navigationService;
    }
}
