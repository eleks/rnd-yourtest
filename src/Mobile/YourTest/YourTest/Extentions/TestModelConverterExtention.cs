using System;

using Xamarin.Forms;
using YourTest.ViewModels.ActiveTest;
using YourTest.Models;
using System.Collections.Generic;

namespace YourTest.Extentions
{
    public static class TestModelConverterExtention
    {
        public static TestViewModel ToViewModel(this Test test)
        {
            var testVM = new TestViewModel()
            {
                Name = test.Name,
                Questions = new List<BaseQuestionViewModel>()
            };

            foreach (var question in test.Questions)
            {
                if (question.Type == QuestionType.Text)
                {
                    var questionVM = new TextQuestionViewModel()
                    {
                        Description = question.Description,
                        PossibleAnswers = question.PossibleAnswers
                    };
                    testVM.Questions.Add(questionVM);
                }
                else if (question.Type == QuestionType.MixedReality)
                {
                    var questionVM = new MixedRealityQuestionViewModel()
                    {
                        Description = question.Description,
                        PossibleAnswers = question.PossibleAnswers
                    };
                    testVM.Questions.Add(questionVM);
                }
            }
            return testVM;
        }
    }
}

