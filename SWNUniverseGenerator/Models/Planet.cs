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
        public String Id { get; set; }

        /// <summary>
        /// This is the parent Star that a Planet orbits. This allows for a partially non-relational database
        /// but still allows you to tie planets to Stars
        /// </summary>
        public String StarId { get; set; }

        /// <summary>
        /// Implemented from ILocation. This should be a unique name for a Planet
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// This implements a World class from WorldInfo.cs and specifies the World Tag of a Planet
        /// This will include a Type and a Description of the Tag, as well as potential Enemies, Friends
        /// and Complications
        /// </summary>
        public String FirstWorldTag { get; set; }

        /// <summary>
        /// This implements a World class from WorldInfo.cs and specifies the World Tag of a Planet
        /// This will include a Type and a Description of the Tag, as well as potential Enemies, Friends
        /// and Complications
        /// </summary>
        public String SecondWorldTag { get; set; }

        /// <summary>
        /// This implements an Atmosphere class from WorldInfo.cs and specifies the Atmosphere of a Planet
        /// </summary>
        public String Atmosphere { get; set; }

        /// <summary>
        /// This implements a Temperature class from WorldInfo.cs and specifies the Temperature of a Planet
        /// </summary>
        public String Temperature { get; set; }

        /// <summary>
        /// This implements a Biosphere class from WorldInfo.cs and specifies the Biosphere of a Planet
        /// </summary>
        public String Biosphere { get; set; }

        /// <summary>
        /// This implements a Population class from WorldInfo.cs and specifies the Population of a Planet
        /// </summary>
        public String Population { get; set; }

        /// <summary>
        /// This implements a TechLevel class from WorldInfo.cs and specifies the Tech Level for a Planet
        /// </summary>
        public String TechLevel { get; set; }

        /// <summary>
        /// This is a marker to create a primary world for system
        /// </summary>
        public Boolean IsPrimary { get; set; }

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

        public Society Society { get; set; }
        
        public Ruler Ruler { get; set; }

        public Ruled Ruled { get; set; }
        
        public Flavor Flavor { get; set; }
    }

    public class Society
    {
        public String PriorCulture { get; set; }
    
        public String OtherSociety { get; set; }
    
        public String MainRemnant { get; set; }
    
        public String SocietyAge { get; set; }
    
        public String ImportantResource { get; set; }
    
        public String FoundingReason { get; set; }
    }
    
    public class Ruler
    {
        public String GeneralSecurity { get; set; }
    
        public String LegitimacySource { get; set; }
    
        public String MainRulerConflict { get; set; }
    
        public String RuleCompletion { get; set; }
    
        public String RuleForm { get; set; }
    
        public String MainPopConflict { get; set; }
    }
    
    public class Ruled
    {
        public String Contentment { get; set; }
        
        public String LastMajorThreat { get; set; }
        
        public String Power { get; set; }
        
        public String Uniformity { get; set; }
        
        public String MainConflict { get; set; }
        
        public String Trends { get; set; }
    }
    
    public class Flavor
    {
        public String BasicFlavor { get; set; }
        
        public String OutsiderTreatment { get; set; }
    
        public String PrimaryVirtue { get; set; }
    
        public String PrimaryVice { get; set; }
    
        public String XenophiliaDegree { get; set; }
    
        public String PossiblePatron { get; set; }
    
        public String Customs { get; set; }
    }
}