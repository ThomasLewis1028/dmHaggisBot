using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// Character object that stores all of the necessary information about a Character
    /// </summary>
    [Table("Characters")]
    public class Character : IEntity
    {
        public Character()
        {
            Id = this.GenerateId();
        }
        
        /// <summary>
        /// An Enum for a list of Genders. Undefined should never be used.
        /// </summary>
        public enum GenderEnum
        {
            Male,
            Female,
            Undefined
        }
        
        //private Ship ship;

        /// <summary>
        /// The unique String ID of a Character. This should be randomly generated to assure uniqueness.
        /// </summary>
        public String Id { get; set; }
        
        /// <summary>
        /// First name of the Character
        /// </summary>
        public String First { get; set; }
        
        /// <summary>
        /// Last name of the Character
        /// </summary>
        public String Last { get; set; }
        
        /// <summary>
        /// Integer Age of the Character 
        /// </summary>
        public Int32 Age { get; set; }
        
        /// <summary>
        /// A unique String ID of a Planet to mark the Character's birth planet
        /// </summary>
        public String BirthPlanetId { get; set; }
        
        /// <summary>
        /// A unique String ID of an ILocation to mark the Character's current Location.
        /// Location can be a planet, a city, or other various location types. 
        /// </summary>
        public String CurrentLocationId { get; set; }
        
        /// <summary>
        /// A string value of a Character's HairStyle
        /// </summary>
        public String HairStyle { get; set; }
        
        /// <summary>
        /// A string value of a Character's HairColor
        /// </summary>
        public String HairCol { get; set; }
        
        /// <summary>
        /// A string value of a Character's EyeColor
        /// </summary>
        public String EyeCol { get; set; }
        
        /// <summary>
        /// A string value of a Character's Skin Color
        /// </summary>
        public String SkinCol { get; set; }
        
        /// <summary>
        /// A string value of a Character's Height
        /// </summary>
        public String Height { get; set; }
        
        /// <summary>
        /// A string value of a Character's Title
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// A Enum for the Character's Gender.
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Returns the concatenation of the First and Last name without storing it in the Json
        /// </summary>
        [JsonIgnore] 
        [NotMapped]
        public String Name => First + " " + Last;
        
        /// <summary>
        /// An integer representation of the likelihood a character will aide in some shady matter
        /// </summary>
        public Int32 CrimeChance { get; set; }

        /// <summary>
        /// A string value for the universe a given character is tied to
        /// </summary>
        public String UniverseId { get; set; }
        
        /// <summary>
        /// A string value for a character's initial reaction to the players
        /// </summary>
        public String InitialReaction { get; set; }
    }
}