using System.Collections.Generic;

namespace dmHaggisBot
{
    public class Star
    {
        private string name;
        private int x, y;
        private List<Planet> planets = new List<Planet>();

        public Star(string name, int x, int y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }

        public List<Planet> Planets
        {
            get => planets;
            set => planets = value;
        }
    }
}