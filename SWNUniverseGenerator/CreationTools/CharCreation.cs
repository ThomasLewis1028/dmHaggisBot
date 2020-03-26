using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    /// <summary>
    /// This class holds the functions for creating Characters and adding them to the Universe.
    /// </summary>
    internal class CharCreation
    {
        private static readonly Random rand = new Random();

        /// <summary>
        /// This function handles all Character creation. Should receive a Universe to edit and a set of
        /// CharacterDefaultSettings that will be used to set defaults.
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="characterDefaultSettings"></param>
        /// <returns>The newly modified universe</returns>
        public Universe AddCharacters(Universe universe, CharacterDefaultSettings characterDefaultSettings)
        {
            // If no Characters have been created on the Universe then give it an empty list of them.
            if (universe.Characters == null)
                universe.Characters = new List<Character>();

            // Load the list of Data from the charData.json
            var charData = LoadCharData();

            // Set the number of characters you want to create. Default is 1.
            var count = characterDefaultSettings.Count < 0
                ? 1
                : characterDefaultSettings.Count;

            var cCount = 0;
            while (cCount < count)
            {
                // Create the character with the specified first and last name
                var character = new Character();

                // Generate a randomized ID for the new Character
                IDGen.GenerateID(character);

                // If the created ID happens to exist (unlikely) then just continue and create a new one
                if (universe.Characters.Exists(a => a.ID == character.ID))
                    continue;

                // Set sheet bounds including sheet# and row count.
                // Must be done here to randomly select the sheet
                var gender = characterDefaultSettings.Gender != Character.GenderEnum.Undefined
                    ? characterDefaultSettings.Gender == Character.GenderEnum.Male ? 0 : 1
                    : rand.Next(0, 2);

                // Get the list of names for the specified gender
                var firstNameList = gender == 0 ? charData.MaleName : charData.FemaleName;

                // Markov stuff; doesn't work right now
                // var model = new StringMarkovNames(1);
                // model.SplitTokens("");
                // model.Learn(firstNameList);
                //
                // Console.WriteLine(model.Walk().First());

                // Set the number of items in each list to be used for the max value in rand.Next()
                var firstCount = firstNameList.Count;
                var lastCount = charData.LastName.Count;
                var hairColorCount = charData.HairColor.Count;
                var hairStyleCount = charData.HairStyle.Count;
                var eyeColorCount = charData.EyeColor.Count;

                // Grab random values from their respective lists or use provided values
                character.First = string.IsNullOrEmpty(characterDefaultSettings.First)
                    ? firstNameList[rand.Next(0, firstCount - 1)]
                    : characterDefaultSettings.First;
                character.Last = string.IsNullOrEmpty(characterDefaultSettings.Last)
                    ? charData.LastName[rand.Next(0, lastCount - 1)]
                    : characterDefaultSettings.Last;
                character.Age = characterDefaultSettings.Age == null || characterDefaultSettings.Age.Length == 0 ||
                                characterDefaultSettings.Age[0] == -1 ||
                                characterDefaultSettings.Age[1] == -1
                    // This creates Ages on a bell-curve where it's more likely to land somewhere around 30-45
                    // with a minimum of 15
                    ? rand.Next(5, 22) + rand.Next(5, 23) + rand.Next(5, 23)
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
                character.BirthPlanet = universe.Planets?[rand.Next(0, universe.Planets.Count)].ID;
                character.CurrentLocation = universe.Planets?[rand.Next(0, universe.Planets.Count)].ID;

                // Add the Character to the list of Characters in the universe
                universe.Characters.Add(character);

                cCount++;
            }

            // Re-order the list of Characters by their first name.
            universe.Characters = universe.Characters.OrderBy(c => c.First).ToList();

            return universe;
        }

        /// <summary>
        /// This grabs all of the information out of the characterData.json and places it into a CharData object
        /// after deserializing it.
        /// </summary>
        /// <returns>A deserialized CharData object</returns>
        private CharData LoadCharData()
        {
            var charData =
                JObject.Parse(
                    File.ReadAllText(@"Data\CharacterData.json"));

            return JsonConvert.DeserializeObject<CharData>(charData.ToString());
        }
    }
}