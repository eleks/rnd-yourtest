using System;
using System.Collections.Generic;

namespace YourTest.REST.Models
{
    public class Question : ModelBase
    {
        public String Description { get; set; }

        public IList<String> PossibleAnswares { get; set; }

        public String Answare { get; set; }

        public Question()
        {
            PossibleAnswares = new List<String>();
        }
    }
}