using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace dmHaggisBot
{
    public class CharCreation
    {
        private static Random rand = new Random();

        //Properties file
        private static readonly JObject prop =
            JObject.Parse(
                File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\properties.json"));

        //Data out of the universe/person json
        private static string personData = (string) prop.GetValue("personData");

        public List<Character> Creation(List<Character> people)
        {
            Excel personExcel = new Excel(personData);
            var personReader = personExcel.ReaderReturn(personData);

            Console.Out.Write("How many characters would you like to create? > ");
            int cc = Int32.Parse(Console.ReadLine());

            var cCount = 0;
            while (cCount < cc)
            {
                //Set sheet bounds inluding sheet# and row count.
                //Must be done here to randomly select the sheet
                var sheet = rand.Next(0, 2);
                var firstLen = personReader.Tables[sheet].Rows.Count;
                var lastLen = personReader.Tables[2].Rows.Count;
                var colLen = personReader.Tables[3].Rows.Count;
                var lenLen = personReader.Tables[4].Rows.Count;
                var eyeLen = personReader.Tables[5].Rows.Count;

                //Create the specified object
                Character character =
                    new Character(
                        personReader.Tables[sheet].Rows[rand.Next(0, firstLen - 1)].ItemArray[0].ToString(),
                        personReader.Tables[2].Rows[rand.Next(0, lastLen - 1)].ItemArray[0].ToString());

                character.Age = rand.Next(10, 95);
                character.Gender = (Character.GenderEnum) sheet;
                character.HairCol = personReader.Tables[3].Rows[rand.Next(0, colLen)].ItemArray[0].ToString();
                character.HairStyle = personReader.Tables[4].Rows[rand.Next(0, lenLen)].ItemArray[0].ToString();
                character.EyeCol = personReader.Tables[5].Rows[rand.Next(0, eyeLen)].ItemArray[0].ToString();

                people.Add(character);

                
                Console.Out.WriteLine("\t{0}, {1}, {2}, {3} {4}, {5} Eyes", character.First + " " + character.Last,
                    character.Gender,
                    character.Age, character.HairCol, character.HairStyle, character.EyeCol);

                cCount++;
            }

            return people;
        }
    }
}