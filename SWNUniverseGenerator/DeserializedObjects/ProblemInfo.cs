using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    /// <summary>
    /// This is the class where a problemData.json will be deserialized to
    /// </summary>
    public class ProblemInfo
    {
        /// <summary>
        /// Store a list of Conflicts to be randomly pulled from
        /// </summary>
        public List<Conflict> Conflicts { get; set; }

        /// <summary>
        /// Store a list of Restraints to be randomly pulled from
        /// </summary>
        public List<String> Restraints { get; set; }

        /// <summary>
        /// Store a list of Twists to be randomly pulled from
        /// </summary>
        public List<String> Twists { get; set; }
    }

    /// <summary>
    /// When a ProblemInfo class is created, the Conflicts will be loaded with these variables
    /// </summary>
    public class Conflict
    {
        /// <summary>
        /// The type of Conflict that was loaded
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// Store a list of Situations about the Conflict to be randomly pulled from
        /// </summary>
        public List<String> Situations { get; set; }

        /// <summary>
        /// Store a list of Focuses about the Conflict to be randomly pulled from
        /// </summary>
        public List<String> Focuses { get; set; }
    }
}