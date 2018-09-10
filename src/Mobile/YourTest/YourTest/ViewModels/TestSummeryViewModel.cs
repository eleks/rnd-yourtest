using System;
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

        public TestSummeryViewModel()
        {
        }
    }
}
