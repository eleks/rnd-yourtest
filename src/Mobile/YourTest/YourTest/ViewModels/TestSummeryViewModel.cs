using System;
using Prism.Navigation;
using YourTest.Models;

namespace YourTest.ViewModels
{
    public class TestSummeryViewModel : ViewModelBase
    {
        private String _name;
        public String Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private Int32 _correctAnswers;
        public Int32 CorrectAnswers
        {
            get => _correctAnswers;
            set => SetProperty(ref _correctAnswers, value);
        }

        private Int32 _questionCount;
        public Int32 QuestionCount
        {
            get => _questionCount;
            set => SetProperty(ref _questionCount, value);
        }

        private Boolean _hasPassed;
        public Boolean HasPassed
        {
            get => _hasPassed;
            private set => SetProperty(ref _hasPassed, value);
        }

        protected override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (parameters[nameof(TestSummery)] is TestSummery summery)
            {
                Populate(summery);
            }
        }

        private void Populate(TestSummery summery)
        {

            Name = summery.Name;
            CorrectAnswers = summery.CorrectAnswersCount;
            QuestionCount = summery.QuestionCount;
            HasPassed = summery.State == TestState.Passed;
        }
    }
}
