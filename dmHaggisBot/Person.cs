namespace dmHaggisBot
{
    public class Person
    {
        private string name;
        private string age;
        private Planet location;

        public Person(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Age
        {
            get => age;
            set => age = value;
        }

        public Planet Location
        {
            get => location;
            set => location = value;
        }
    }
}