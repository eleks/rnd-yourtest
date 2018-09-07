using Prism.Navigation;
using YourTest.Models;
using YourTest.Extentions;
using System.Windows.Input;
using Prism.Commands;
using YourTest.REST;
using System;
using System.Collections.Generic;
using YourTest.Manager;
using Autofac;
using Prism.Ioc;
using Prism.Autofac;

namespace YourTest.ViewModels.ActiveTest
{
    public class ActiveTestPageViewModel : ViewModelBase, INavigatingAware
    {
        public TestViewModel Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }

        public String LocalIPAddress => _ipAddressManager.GetIPAddress();

        public ICommand SelectQuestionCommand { get; set; }

        public ActiveTestPageViewModel(ITestsRest testsRest,
                                       IContainerRegistry containerRegistry,
                                       IIPAddressManager addressManager)
        {
            _container = containerRegistry.GetContainer();
            _testsRest = testsRest;
            _ipAddressManager = addressManager;

            SelectQuestionCommand = new DelegateCommand<String>(HandleAction);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters["test"] is Test test)
            {
                Test = test.ToViewModel(_container);
            }
        }

        private void HandleAction(String answer)
        {
            _answers.Add(new QuestionAnswer { Answer = answer });
        }


        private List<QuestionAnswer> _answers = new List<QuestionAnswer>();
        private TestViewModel _test;
        private readonly IContainer _container;
        private readonly ITestsRest _testsRest;
        private readonly IIPAddressManager _ipAddressManager;
    }
}
