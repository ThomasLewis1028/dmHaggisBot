using System.Collections.Generic;

namespace dmHaggisBot
{
    public class OtherWorlds
    {
        public List<Origin> Origins;
        public List<Relationship> Relationships;
        public List<Contact> Contacts;


        public List<Contact> Contact1
        {
            get => Contacts;
            set => Contacts = value;
        }

        public List<Relationship> Relationships1
        {
            get => Relationships;
            set => Relationships = value;
        }

        public List<Origin> Origin1
        {
            get => Origins;
            set => Origins = value;
        }
    }

    public class Origin
    {
        private string origin;

        public string Origin1
        {
            get => origin;
            set => origin = value;
        }
    }

    public class Relationship
    {
        private string relationship;

        public string Relationship1
        {
            get => relationship;
            set => relationship = value;
        }
    }
    
    public class Contact
    {
        private string contact;

        public string Contact1
        {
            get => contact;
            set => contact = value;
        }
    }
}