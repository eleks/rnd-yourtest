using System;
using YourTest.REST.Models;
using YourTest.REST.Repository;
using System.Collections.Generic;

namespace YourTest.REST.Data
{
    public class DemoDataProvider : IDataProvider<Test>
    {
        public void Seed(IRepository<Test> repository)
        {
            List<Test> demoData = new List<Test>();
            demoData.Add(CreateEasyTest());

            foreach (var d in demoData)
            {
                repository.Insert(d);
            }
        }

        private Test CreateEasyTest()
        {
            return new Test
            {
                Id = 1,
                Name = "Easy Test",
                Questions = new List<Question>
                    {
                        new Question
                        {
                            Id = 1,
                            Description = "What sky color is?",
                            Answare = "blue",
                            PossibleAnswares = new []
                            {
                                "grean",
                                "red",
                                "blue",
                                "pink"
                            }
                        },
                        new Question
                        {
                            Id = 2,
                            Description = "2 + 2 = ?",
                            Answare = "4",
                            PossibleAnswares = new []
                            {
                                "3",
                                "8",
                                "6",
                                "4"
                            }
                        },
                    }
            };
        }
    }
}
