using System;
using YourTest.REST;
using System.Collections.ObjectModel;
using YourTest.Models;
using System.Windows.Input;
using Prism.Commands;
using System.Threading.Tasks;
using MvvmHelpers;

namespace YourTest.ViewModels
{
    public class TestsListViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Test> Source { get; } = new ObservableRangeCollection<Test>();

        public ICommand LoadCommand { get; }


        public TestsListViewModel(ITestsRest testsRest)
        {
            _testsRest = testsRest;

            LoadCommand = new DelegateCommand(async () => await LoadAsync());

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
            Source.AddRange(testsData);
            IsBusy = false;
        }


        private readonly ITestsRest _testsRest;
    }
}
