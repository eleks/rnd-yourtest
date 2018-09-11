using System;
using System.Collections.Generic;

namespace YourTest.Models
{
    public class Test : ModelBase
    {
        public String Name { get; set; }

        public Double PersentPassageThreshold { get; set; }

        public IList<Question> Questions { get; set; }

        public Test()
        {
            Questions = new List<Question>();
        }
    }
}
