using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Prism.Navigation;
using YourTest.Models;
using YourTest.Extentions;

namespace YourTest.ViewModels.ActiveTest
{
    public class ActiveTestPageViewModel : ViewModelBase, INavigatedAware
    {
        public TestViewModel Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }

        private TestViewModel _test;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var test = parameters["test"] as Test;
            if (test != null)
            {
                Test = test.ToViewModel();
            }
        }
    }
}
