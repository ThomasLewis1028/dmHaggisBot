namespace dmHaggisBot
{
    public class WorldTag
    {
        private string type;
        private string description;
        private string[] enemies;
        private string[] friends;
        private string[] complications;
        private string[] things;
        private string[] places;

        public string Type
        {
            get => type;
            set => type = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string[] Enemies
        {
            get => enemies;
            set => enemies = value;
        }

        public string[] Friends
        {
            get => friends;
            set => friends = value;
        }

        public string[] Complications
        {
            get => complications;
            set => complications = value;
        }

        public string[] Things
        {
            get => things;
            set => things = value;
        }

        public string[] Places
        {
            get => places;
            set => places = value;
        }
    }
}