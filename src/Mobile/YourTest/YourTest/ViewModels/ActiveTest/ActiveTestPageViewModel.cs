﻿using Prism.Navigation;
using YourTest.Models;
using System.Windows.Input;
using Prism.Commands;
using YourTest.REST;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourTest.Navigation;

namespace YourTest.ViewModels.ActiveTest
{
    public class ActiveTestPageViewModel : ViewModelBase
    {
        public TestViewModel Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }

        public Int32 ActiveQuestionIndex
        {
            get => _activeQuestionIndex;
            set => SetProperty(ref _activeQuestionIndex, value);
        }

        public ICommand SelectQuestionCommand { get; set; }
        public ICommand CompliteTestCommand { get; set; }

        public ActiveTestPageViewModel(ITestsRest testsRest,
                                       TestViewModelFactory testVMFactory,
                                       INavigationService navigationService)
        {
            _tesVMFactory = testVMFactory;
            _testsRest = testsRest;
            _navigationService = navigationService;

            SelectQuestionCommand = new DelegateCommand<String>(HandleAction);
            CompliteTestCommand = new DelegateCommand(async () => await ComplteTestAsync());
        }

        protected override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters[nameof(Test)] is Test test)
            {
                Test = _tesVMFactory.CreatVM(test);
            }
        }

        private async Task ComplteTestAsync()
        {
            IsBusy = true;
            try
            {
                var result = await _testsRest.ProcessTestAsync(Test.Id, _answers);
                await _navigationService.NavigateAsync<TestSummaryViewModel>(
                    closeCurrent: true,
                    navParams: new NavigationParameters
                    {
                        { nameof(TestSummary), result }
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        private void HandleAction(String answer)
        {
            var index = ActiveQuestionIndex;
            var question = Test.Questions[index];
            _answers.Add(new QuestionAnswer
            {
                Id = question.Id,
                Answer = answer
            });

            ActiveQuestionIndex++;
        }

        private List<QuestionAnswer> _answers = new List<QuestionAnswer>();
        private TestViewModel _test;
        private int _activeQuestionIndex;
        private readonly TestViewModelFactory _tesVMFactory;
        private readonly ITestsRest _testsRest;
        private readonly INavigationService _navigationService;
    }
}
