using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YourTest.Models
{
    public class Question : ModelBase
    {
        public String Description { get; set; }
        public IList<String> PossibleAnswers { get; set; }
        public QuestionType Type { get; set; }

        public Question()
        {
            PossibleAnswers = new List<String>();
        }
    }
}