using System;
using SWNUniverseGenerator.DeserializedObjects;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// Planet object that stores all of the necessary information about a Planet
    /// </summary>
    public class Planet : ILocation
    {
        /// <summary>
        /// Implemented from ILocation. Will be the unique ID for a Planet
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// This is the parent Star that a Planet orbits. This allows for a partially non-relational database
        /// but still allows you to tie planets to Stars
        /// </summary>
        public String StarID { get; set; }

        /// <summary>
        /// Implemented from ILocation. This should be a unique name for a Planet
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// This implements a World class from WorldInfo.cs and specifies the World Tag of a Planet
        /// This will include a Type and a Description of the Tag, as well as potential Enemies, Friends
        /// and Complications
        /// </summary>
        public WorldTag FirstWorldTag { get; set; }
        
        /// <summary>
        /// This implements a World class from WorldInfo.cs and specifies the World Tag of a Planet
        /// This will include a Type and a Description of the Tag, as well as potential Enemies, Friends
        /// and Complications
        /// </summary>
        public WorldTag SecondWorldTag { get; set; }

        /// <summary>
        /// This implements an Atmosphere class from WorldInfo.cs and specifies the Atmosphere of a Planet
        /// </summary>
        public Atmosphere Atmosphere { get; set; }

        /// <summary>
        /// This implements a Temperature class from WorldInfo.cs and specifies the Temperature of a Planet
        /// </summary>
        public Temperature Temperature { get; set; }

        /// <summary>
        /// This implements a Biosphere class from WorldInfo.cs and specifies the Biosphere of a Planet
        /// </summary>
        public Biosphere Biosphere { get; set; }

        /// <summary>
        /// This implements a Population class from WorldInfo.cs and specifies the Population of a Planet
        /// </summary>
        public Population Population { get; set; }

        /// <summary>
        /// This implements a TechLevel class from WorldInfo.cs and specifies the Tech Level for a Planet
        /// </summary>
        public TechLevel TechLevel { get; set; }

        /// <summary>
        /// This is the "Origin" of the planet from the Primary
        /// </summary>
        public String Origin { get; set; }

        /// <summary>
        /// This is the "Relationship" between the Primary Planet and a non-Primary Planet
        /// </summary>
        public String Relationship { get; set; }

        /// <summary>
        /// This is a "Point of Contact" between the Primary Planet and a non-Primary Planet
        /// </summary>
        public String Contact { get; set; }
    }
}