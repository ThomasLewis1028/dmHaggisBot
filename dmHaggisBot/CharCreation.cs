using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace dmHaggisBot
{
    internal class CharCreation
    {
        private static Random rand = new Random();

        //Properties file
        private static readonly JObject prop =
            JObject.Parse(
                File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\properties.json"));

        //Data out of the universe/person json
        private static string personData = (string) prop.GetValue("personData");

        public Universe AddCharacter(Universe universe, CharacterDefaultSettings characterDefaultSettings)
        {
            Excel personExcel = new Excel(personData);
            var personReader = personExcel.ReaderReturn(personData);

            var cCount = 0;
            while (cCount < characterDefaultSettings.Count)
            {
                //Set sheet bounds including sheet# and row count.
                //Must be done here to randomly select the sheet
                var sheet = characterDefaultSettings.Gender != Character.GenderEnum.Undefined
                    ? (characterDefaultSettings.Gender == Character.GenderEnum.Male ? 0 : 1)
                    : rand.Next(0, 2);
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

                character.Age = characterDefaultSettings.Age == null || characterDefaultSettings.Age.Length == 0
                    ? rand.Next(15, 46) + rand.Next(0, 46)
                    : rand.Next(Int32.Parse(characterDefaultSettings.Age[0]),
                        Int32.Parse(characterDefaultSettings.Age[1]));
                character.Gender = (Character.GenderEnum) sheet;
                character.HairCol = string.IsNullOrEmpty(characterDefaultSettings.HairCol)
                    ? personReader.Tables[3].Rows[rand.Next(0, colLen)].ItemArray[0].ToString()
                    : characterDefaultSettings.HairCol;
                character.HairStyle = string.IsNullOrEmpty(characterDefaultSettings.HairStyle)
                    ? personReader.Tables[4].Rows[rand.Next(0, lenLen)].ItemArray[0].ToString()
                    : characterDefaultSettings.HairStyle;
                character.EyeCol = string.IsNullOrEmpty(characterDefaultSettings.EyeCol)
                    ? personReader.Tables[5].Rows[rand.Next(0, eyeLen)].ItemArray[0].ToString()
                    : characterDefaultSettings.EyeCol;
                character.Title = string.IsNullOrEmpty(characterDefaultSettings.Title)
                    ? null
                    : characterDefaultSettings.Title;
                // character.BirthPlace = (a => universe.Stars[].Planets[])

                universe.Characters.Add(character);

                Console.Out.WriteLine("\t{0}, {1}, {2}, {3} {4}, {5} Eyes", character.First + " " + character.Last,
                    character.Gender,
                    character.Age, character.HairCol, character.HairStyle, character.EyeCol);

                cCount++;
            }

            return universe;
        }
    }
}