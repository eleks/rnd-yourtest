using System;
using System.Collections.Generic;

namespace YourTest.ViewModels.ActiveTest
{
    public abstract class BaseQuestionViewModel
    {
        public String Description { get; set; }
        public IList<String> PossibleAnswers { get; set; }
    }
}
