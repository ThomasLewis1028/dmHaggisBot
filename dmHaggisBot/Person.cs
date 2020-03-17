using System;
using System.Runtime.Serialization;

namespace dmHaggisBot
{
    public class Person
    {
        public enum GenderEnum{Male, Female}
        
        private string first;
        private string last;
        private int age;
        private Planet location;
        private string hairStyle;
        private string hairCol;
        private string eyeCol;
        private string title;
        private GenderEnum gender;
        //private Ship ship;
        

        public Person(string first, string last)
        {
            this.first = first;
            this.last = last;
        }

        
        public string First
        {
            get => first;
            set => first = value;
        }

        public string Last
        {
            get => last;
            set => last = value;
        }

        public string HairCol
        {
            get => hairCol;
            set => hairCol = value;
        }

        public string HairStyle
        {
            get => hairStyle;
            set => hairStyle = value;
        }

        public string EyeCol
        {
            get => eyeCol;
            set => eyeCol = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public Planet Location
        {
            get => location;
            set => location = value;
        }

        public GenderEnum Gender
        {
            get => gender;
            set => gender = value;
        }
    }
}