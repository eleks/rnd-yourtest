using YourTest.ViewModels.ActiveTest;
using YourTest.Models;
using System.Collections.Generic;
using Autofac;

namespace YourTest.Extentions
{
    public static class TestModelConverterExtention
    {
        public static TestViewModel ToViewModel(this Test test, IContainer container)
        {
            var testVM = new TestViewModel()
            {
                Id = test.Id,
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
                    var questionVM = container.Resolve<MixedRealityQuestionViewModel>();
                    questionVM.Description = question.Description;
                    questionVM.PossibleAnswers = question.PossibleAnswers;
                    testVM.Questions.Add(questionVM);
                }
            }
            return testVM;
        }
    }
}

