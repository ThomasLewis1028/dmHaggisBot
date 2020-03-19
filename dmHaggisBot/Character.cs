using System;
using System.Runtime.Serialization;

namespace dmHaggisBot
{
    public class Character
    {
        public enum GenderEnum
        {
            Male,
            Female,
            Undefined
        }

        public String First { get; set; }
        public String Last { get; set; }
        public Int32 Age { get; set; }
        public Planet BirthPlace { get; set; }
        public Planet Location { get; set; }
        public String HairStyle { get; set; }
        public String HairCol { get; set; }
        public String EyeCol { get; set; }
        public String Title { get; set; }
        public GenderEnum Gender { get; set; }
        //private Ship ship;

        public Character(string first, string last)
        {
            First = first;
            Last = last;
        }

        public string Name
        {
            get => First + " " + Last;
        }
    }
}