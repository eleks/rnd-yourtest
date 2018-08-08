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

        public async Task<IEnumerable<Test>> GetAllAsync() => await Task.Run<IEnumerable<Test>>(() => _repo.Query().Select(PreperItem).ToArray());

        public async Task<Test> GetByIdAync(int id) => await Task.Run(() => PreperItem(_repo.Query().FirstOrDefault(t => t.Id == id)));

        public TestSummery Verify(int testId, Test testToProcess)
        {
            var originTest = _repo.Query().FirstOrDefault(t => t.Id == testId)
                ?? throw new InvalidOperationException($"No test found for {testId}");

            var summery = new TestSummery()
            {
                Name = originTest.Name,
                QuestionCount = originTest.Questions.Count
            };

            var correctCount = 0;
            var questionDic = originTest.Questions.ToDictionary(q => q.Id);
            foreach (var aq in testToProcess.Questions)
            {
                Question origenQuestion = null;
                if (!questionDic.TryGetValue(aq.Id, out origenQuestion))
                {
                    continue;
                }

                var isAnswerCorrect = origenQuestion.Answare == aq.Answare;
                if (!isAnswerCorrect)
                {
                    continue;
                }
                correctCount++;
            }

            summery.CorrectAnswersCount = correctCount;

            return summery;
        }


        private static Test PreperItem(Test item)
        {
            if (item == null)
            {
                return null;
            }

            var res = item;
            foreach (var q in res.Questions)
            {
                q.Answare = null;
            }
            return res;
        }

        private readonly IRepository<Test> _repo;
    }
}
