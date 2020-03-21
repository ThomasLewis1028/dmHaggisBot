using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SWNUniverseGenerator
{
    internal class CharCreation
    {
        private static readonly Random rand = new Random();

        public Universe AddCharacter(Universe universe, CharacterDefaultSettings characterDefaultSettings)
        {
            if (universe.Characters == null)
                universe.Characters = new List<Character>();

            var charData = LoadCharData();

            var count = characterDefaultSettings.Count < 0
                ? 1
                : characterDefaultSettings.Count;

            // List<Planet> planets = null;
            //
            // if (universe.Planets != null)
            //      planets = universe.Planets.ToList();


            var cCount = 0;
            while (cCount < count)
            {
                //Set sheet bounds including sheet# and row count.
                //Must be done here to randomly select the sheet
                var gender = characterDefaultSettings.Gender != Character.GenderEnum.Undefined
                    ? characterDefaultSettings.Gender == Character.GenderEnum.Male ? 0 : 1
                    : rand.Next(0, 2);

                var firstNameList = gender == 0 ? charData.MaleName : charData.FemaleName;
                
                // var model = new StringMarkovNames(1);
                // model.SplitTokens("");
                // model.Learn(firstNameList);
                //
                // Console.WriteLine(model.Walk().First());
                
                var firstCount = firstNameList.Count;
                var lastCount = charData.LastName.Count;
                var hairColorCount = charData.HairColor.Count;
                var hairStyleCount = charData.HairStyle.Count;
                var eyeColorCount = charData.EyeColor.Count;
                
                //Create the specified object
                var first = string.IsNullOrEmpty(characterDefaultSettings.First)
                    ? firstNameList[rand.Next(0, firstCount - 1)]
                    : characterDefaultSettings.First;

                var last = string.IsNullOrEmpty(characterDefaultSettings.Last)
                    ? charData.LastName[rand.Next(0, lastCount - 1)]
                    : characterDefaultSettings.Last;

                var character = new Character(first, last);
                
                if (universe.Characters.Exists(a => a.Name == character.Name))
                    continue;
                
                IDGen.GenerateID(character);
                
                character.Age = characterDefaultSettings.Age == null || characterDefaultSettings.Age.Length == 0 ||
                                characterDefaultSettings.Age[0] == -1 ||
                                characterDefaultSettings.Age[1] == -1
                    ? rand.Next(15, 31) + rand.Next(0, 31) + rand.Next(0, 31)
                    : rand.Next(characterDefaultSettings.Age[0], characterDefaultSettings.Age[1]);
                character.Gender = (Character.GenderEnum) gender;
                character.HairCol = string.IsNullOrEmpty(characterDefaultSettings.HairCol)
                    ? charData.HairColor[rand.Next(0, hairColorCount)]
                    : characterDefaultSettings.HairCol;
                character.HairStyle = string.IsNullOrEmpty(characterDefaultSettings.HairStyle)
                    ? charData.HairStyle[rand.Next(0, hairStyleCount)]
                    : characterDefaultSettings.HairStyle;
                character.EyeCol = string.IsNullOrEmpty(characterDefaultSettings.EyeCol)
                    ? charData.EyeColor[rand.Next(0, eyeColorCount)]
                    : characterDefaultSettings.EyeCol;
                character.Title = string.IsNullOrEmpty(characterDefaultSettings.Title)
                    ? null
                    : characterDefaultSettings.Title;
                character.BirthPlanet = universe.Planets == null
                    ? null
                    : universe.Planets[rand.Next(0, universe.Planets.Count)].ID;
                character.CurrentLocation = universe.Planets == null
                    ? null
                    : universe.Planets[rand.Next(0, universe.Planets.Count)].ID;
                
                universe.Characters.Add(character);
                
                cCount++;
            }

            universe.Characters = universe.Characters.OrderBy(c => c.First).ToList();

            return universe;
        }

        private CharData LoadCharData()
        {
            var charData =
                JObject.Parse(
                    File.ReadAllText(@"Data\CharacterData.json"));

            return JsonConvert.DeserializeObject<CharData>(charData.ToString());
        }
    }
}