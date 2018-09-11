using System;
namespace YourTest.REST.Models
{
    public class TestSummery
    {
        public Int32 TestId { get; set; }
        public String Name { get; set; }
        public Int32 QuestionCount { get; set; }
        public Int32 CorrectAnswersCount { get; set; }
    }
}
