using System;
using System.Collections.Generic;

namespace YourTest.ViewModels.ActiveTest
{
    public abstract class BaseQuestionViewModel : ViewModelBase
    {
        public Int32 Id { get; set; }
        public String Description { get; set; }
        public IList<String> PossibleAnswers { get; set; }
    }
}
