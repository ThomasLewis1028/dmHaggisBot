﻿using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    /// This holds the parameters that can be used in creating a Character
    /// </summary>
    public class CharacterDefaultSettings
    {
        /// <summary>
        /// This value should be a string for a first name
        /// </summary>
        public String First { get; set; }

        /// <summary>
        /// This value should be a string for the last name
        /// </summary>
        public String Last { get; set; }

        /// <summary>
        /// This value should be a range using a string.
        /// Generally this will be between 15 and 100
        /// </summary>
        public Int32[] Age { get; set; }

        /// <summary>
        /// This value should be a description of the hairstyle
        /// </summary>
        public String HairStyle { get; set; }

        /// <summary>
        /// This value should be a description of the hair color
        /// </summary>
        public String HairCol { get; set; }

        /// <summary>
        /// This value should be a description of the eye color
        /// </summary>
        public String EyeCol { get; set; }

        /// <summary>
        /// This value should be a character's title
        /// Examples: Captain, Engineer, etc
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// This will be an ENUM for the gender.
        /// 0 == Male, 1 == Female
        /// </summary>
        public Character.GenderEnum Gender { get; set; }

        /// <summary>
        /// This value should be the number of times you will create a new character with the given values
        /// </summary>
        public Int32 Count { get; set; }
        
        /// <summary>
        /// This value should be the range that you wish to randomly pick from to create a CrimeChance
        /// </summary>
        public Int32[] CrimeChance { get; set; }
        
        /// <summary>
        /// This value should be a specific ShipID that you wish you tie a character to
        /// </summary>
        public String ShipId { get; set; }
    }
}