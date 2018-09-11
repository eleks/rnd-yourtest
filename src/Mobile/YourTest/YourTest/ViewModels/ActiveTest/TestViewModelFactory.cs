﻿using System;
using System.Collections.Generic;
using Prism.Ioc;
using YourTest.Models;
using System.Linq;

namespace YourTest.ViewModels.ActiveTest
{
    public class TestViewModelFactory
    {
        public TestViewModel CreatVM(Test test)
        {
            List<BaseQuestionViewModel> questions = test
                .Questions
                .Select(q => CreateQuestionVM(test, q))
                .ToList();

            return new TestViewModel()
            {
                Id = test.Id,
                Name = test.Name,
                Questions = questions
            };
        }

        public TestViewModelFactory(IContainerExtension containerExtension)
        {
            _containerExtension = containerExtension;

            _questionFactory = new Dictionary<QuestionType, Func<Question, BaseQuestionViewModel>>
            {
                {QuestionType.Text, CreatTextQuestion},
                {QuestionType.MixedReality, CreatMixedRealityQuestion},
            };
        }

        private BaseQuestionViewModel CreateQuestionVM(Test test, Question question)
        {
            if (!_questionFactory.ContainsKey(question.Type))
            {
                throw new InvalidOperationException($"Not supported question type: '{question.Type}' in Test '{test.Name}'");
            }

            var questionVM = _questionFactory[question.Type].Invoke(question);
            return questionVM;
        }

        private BaseQuestionViewModel CreatTextQuestion(Question question)
        {
            return new TextQuestionViewModel()
            {
                Description = question.Description,
                PossibleAnswers = question.PossibleAnswers
            };
        }

        private BaseQuestionViewModel CreatMixedRealityQuestion(Question question)
        {
            var questionVM = _containerExtension.Resolve<MixedRealityQuestionViewModel>();
            questionVM.Description = question.Description;
            questionVM.PossibleAnswers = question.PossibleAnswers;

            return questionVM;
        }

        private readonly Dictionary<QuestionType, Func<Question, BaseQuestionViewModel>> _questionFactory;

        private readonly IContainerExtension _containerExtension;
    }
}
