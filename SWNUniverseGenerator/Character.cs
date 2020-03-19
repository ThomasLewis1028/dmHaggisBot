using System;
using Newtonsoft.Json;

namespace SWNUniverseGenerator
{
    public class Character : IEntity
    {
        public enum GenderEnum
        {
            Male,
            Female,
            Undefined
        }
        //private Ship ship;

        public Character(string first, string last)
        {
            First = first;
            Last = last;
        }

        public String ID { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }
        public String BirthPlanet { get; set; }
        public String CurrentLocation { get; set; }
        public string HairStyle { get; set; }
        public string HairCol { get; set; }
        public string EyeCol { get; set; }
        public string Title { get; set; }
        public GenderEnum Gender { get; set; }

        [JsonIgnore] public string Name => First + " " + Last;

        public override string ToString()
        {
            return base.ToString();
        }
    }
}