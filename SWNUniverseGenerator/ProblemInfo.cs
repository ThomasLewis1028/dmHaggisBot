using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator
{
    public class ProblemInfo
    {
        public List<Conflict> Conflicts { get; set; }

        public List<String> Restraints { get; set; }

        public List<String> Twists { get; set; }
    }

    public class Conflict
    {
        public String Type { get; set; }

        public List<String> Situations { get; set; }

        public List<String> Focuses { get; set; }
    }
}