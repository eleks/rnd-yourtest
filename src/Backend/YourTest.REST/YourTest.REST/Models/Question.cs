using System;
using System.Collections.Generic;

namespace YourTest.REST.Models
{
    public class Question : ModelBase
    {
        public String Description { get; set; }
        public IList<String> PossibleAnswers { get; set; }
        public String Answer { get; set; }
        public QuestionType Type { get; set; }

        public Question()
        {
            PossibleAnswers = new List<String>();
        }
    }
}