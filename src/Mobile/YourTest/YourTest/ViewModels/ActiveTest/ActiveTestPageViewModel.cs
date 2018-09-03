using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace YourTest.ViewModels.ActiveTest
{
    public class ActiveTestPageViewModel : ViewModelBase
    {
        public ActiveTestPageViewModel()
        {
            Test = new TestViewModel()
            {
                Name = "Test Name",
                Questions = new ObservableCollection<BaseQuestionViewModel>(){
                    new TextQuestionViewModel()
                    {
                        Description = "Soem text question",
                        PossibleAnswers = new List<String>(){"a", "b", "c"}},
                    new MixedRealityQuestionViewModel()
                    {
                        Description = " MixedReality question",
                        PossibleAnswers = new List<String>(){ "a", "b", "c"}
                    }
                }
            };
        }

        public TestViewModel Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }

        private TestViewModel _test;
    }
}
