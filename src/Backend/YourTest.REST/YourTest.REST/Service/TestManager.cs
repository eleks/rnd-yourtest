using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourTest.REST.Models;
using YourTest.REST.Repository;
using System.Linq;
using System.Xml.Linq;

namespace YourTest.REST.Service
{
    public class TestManager : ITestManager
    {
        public TestManager(IRepository<Test> repository) => _repo = repository;

        public async Task<IEnumerable<Test>> GetAllAsync() => await Task.Run<IEnumerable<Test>>(() => _repo.Query().ToArray());

        public async Task<Test> GetByIdAync(int id) => await Task.Run(() => _repo.Query().FirstOrDefault(t => t.Id == id));

        public TestSummery Verify(int testId, IEnumerable<QuestionAnswer> answers)
        {
            var originTest = _repo.Query().FirstOrDefault(t => t.Id == testId)
                ?? throw new InvalidOperationException($"No test found for Id: {testId}");

            var summery = new TestSummery()
            {
                Name = originTest.Name,
                QuestionCount = originTest.Questions.Count
            };

            var correctCount = 0;
            var questionDic = originTest.Questions.ToDictionary(q => q.Id);
            foreach (var aq in answers)
            {
                Question origenQuestion = null;
                if (!questionDic.TryGetValue(aq.Id, out origenQuestion))
                {
                    continue;
                }

                var isAnswerCorrect = origenQuestion.Answer == aq.Answer;
                if (!isAnswerCorrect)
                {
                    continue;
                }
                correctCount++;
            }

            summery.CorrectAnswersCount = correctCount;

            return summery;
        }

        private readonly IRepository<Test> _repo;
    }
}
