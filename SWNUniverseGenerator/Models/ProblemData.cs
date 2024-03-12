using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.Models
{
    public class ProblemConflictData : BaseEntity
    {
        public String Type { get; set; }

        public List<String> Situations { get; set; }
        
        public List<String> Focuses { get; set; }
    }

    public class ProblemRestraintData : BaseEntity
    {
        public String Restraint { get; set; }
    }

    public class ProblemTwistData : BaseEntity
    {
        public String Twist { get; set; }
    }
}