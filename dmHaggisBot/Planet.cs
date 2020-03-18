namespace dmHaggisBot
{
    public class Planet
    {
        private string name;
        private WorldTag worldTag;
        private OtherWorlds otherWorlds;

        public Planet(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public WorldTag WorldTag
        {
            get => worldTag;
            set => worldTag = value;
        }

        public OtherWorlds OtherWorlds
        {
            get => otherWorlds;
            set => otherWorlds = value;
        }
    }
}