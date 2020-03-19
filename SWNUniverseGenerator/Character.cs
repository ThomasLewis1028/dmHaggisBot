using Newtonsoft.Json;

namespace SWNUniverseGenerator
{
    public class Character
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

        public string First { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }
        public Planet BirthPlace { get; set; }
        public Planet Location { get; set; }
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