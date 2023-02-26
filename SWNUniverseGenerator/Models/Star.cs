﻿using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This holds all of the necessary information about a Star
    /// </summary>
    public class Star : ILocation
    {
        
        public Star()
        {
            Id = this.GenerateId();
        }
        
        /// <summary>
        /// A unique ID that will be generated when creating a Star
        /// </summary>
        public String Id { get; set; }
        
        /// <summary>
        /// A string value for the universe a given star is tied to
        /// </summary>
        public String UniverseId { get; set; }

        /// <summary>
        /// The string Name of a Star
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// The Color of the Star
        /// </summary>
        public StarColorEnum StarColor { get; set; }
        
        /// <summary>
        /// The Classification of the Star
        /// </summary>
        public StarClassEnum StarClass { get; set; }

        /// <summary>
        /// Enum to star colors
        /// </summary>
        public enum StarColorEnum
        {
            [Description("Blue")]
            Blue,
            [Description("Blue White")]
            BlueWhite,
            [Description("White")]
            White,
            [Description("Yellow White")]
            YellowWhite,
            [Description("Yellow")]
            Yellow,
            [Description("Light Orange")]
            LightOrange,
            [Description("Orange Red")]
            OrangeRed,
            [Description("Undefined")]
            Undefined            
        }

        /// <summary>
        /// Enum to star classification
        /// </summary>
        public enum StarClassEnum
        {
            O,
            B,
            A,
            F,
            G,
            K,
            M,
            Undefined
        }
    }
}