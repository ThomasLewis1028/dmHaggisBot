using System.Collections.Generic;

namespace dmHaggisBot
{
    public class People
    {
        private List<Person> peopleList = new List<Person>();

        public List<Person> PeopleList
        {
            get => peopleList;
            set => peopleList = value;
        }
    }
}