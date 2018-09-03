﻿using System;
using System.Collections.Generic;
using Prism.Mvvm;

namespace YourTest.ViewModels.ActiveTest
{
    public class TestViewModel : BindableBase
    {
        public String Name { get; set; }

        public IList<BaseQuestionViewModel> Questions { get; set; }

        public TestViewModel()
        {
            Questions = new List<BaseQuestionViewModel>();
        }
    }
}
