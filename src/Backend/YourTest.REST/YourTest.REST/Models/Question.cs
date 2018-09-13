using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YourTest.REST.Models
{
    public class Question : ModelBase
    {
        public String Description { get; set; }
        public IList<String> PossibleAnswers { get; set; }
        public QuestionType Type { get; set; }

        // todo add dynamic binder on test response
        // [JsonIgnore]
        public String Answer { get; set; }

        public Question()
        {
            PossibleAnswers = new List<String>();
        }
    }
}