﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private static readonly Random Rand = new Random();

        /// <summary>
        /// This function handles all Character creation. Should receive a Universe to edit and a set of
        /// CharacterDefaultSettings that will be used to set defaults.
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="characterDefaultSettings"></param>
        /// <param name="charData"></param>
        /// <param name="nameGenerations"></param>
        /// <returns>The newly modified universe</returns>
        public Universe AddCharacters(Universe universe, CharacterDefaultSettings characterDefaultSettings,
            CharData charData, List<NameGeneration> nameGenerations)
        {
            // If no Characters have been created on the Universe then give it an empty list of them.
            universe.Characters ??= new List<Character>();

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
                IdGen.GenerateId(character);

                // If the created ID happens to exist (unlikely) then just continue and create a new one
                if (universe.Characters.Exists(a => a.Id == character.Id))
                    continue;

                // Set sheet bounds including sheet# and row count.
                // Must be done here to randomly select the sheet
                var gender = characterDefaultSettings.Gender != Character.GenderEnum.Undefined
                    ? characterDefaultSettings.Gender == Character.GenderEnum.Male ? 0 : 1
                    : Rand.Next(0, 2);

                // Get the list of names for the specified gender
                var firstNameList = gender == 0 ? charData.MaleName : charData.FemaleName;
                var nameGeneration = gender == 0 ? nameGenerations[0] : nameGenerations[1];

                // Markov stuff; doesn't work right now
                // NameGenerator ng = new NameGenerator();
                // Console.Out.WriteLine("NG Created");
                // dynamic chain = ng.construct_chain(firstNameList);
                // Console.Out.WriteLine("Chain Created");
                // Console.Out.WriteLine(ng.markov_name(chain));

                // Set the number of items in each list to be used for the max value in rand.Next()
                var firstCount = firstNameList.Count;
                var lastCount = charData.LastName.Count;
                var hairColorCount = charData.HairColor.Count;
                var hairStyleCount = charData.HairStyle.Count;
                var eyeColorCount = charData.EyeColor.Count;

                // Grab random values from their respective lists or use provided values
                character.First = string.IsNullOrEmpty(characterDefaultSettings.First)
                    ? Rand.Next(0, 4) == 1
                        ? nameGeneration.GenerateName()
                        : firstNameList[Rand.Next(0, firstCount - 1)]
                    : characterDefaultSettings.First;
                character.Last = string.IsNullOrEmpty(characterDefaultSettings.Last)
                    ? charData.LastName[Rand.Next(0, lastCount - 1)]
                    : characterDefaultSettings.Last;
                character.Age = characterDefaultSettings.Age == null || characterDefaultSettings.Age.Length == 0 ||
                                characterDefaultSettings.Age[0] == -1 || characterDefaultSettings.Age[1] == -1
                    // This creates Ages on a bell-curve where it's more likely to land somewhere around 30-45
                    // with a minimum of 15
                    ? Rand.Next(5, 22) + Rand.Next(5, 23) + Rand.Next(5, 23)
                    : Rand.Next(characterDefaultSettings.Age[0], characterDefaultSettings.Age[1]);
                character.Gender = (Character.GenderEnum) gender;
                character.HairCol = string.IsNullOrEmpty(characterDefaultSettings.HairCol)
                    ? charData.HairColor[Rand.Next(0, hairColorCount)]
                    : characterDefaultSettings.HairCol;
                character.HairStyle = string.IsNullOrEmpty(characterDefaultSettings.HairStyle)
                    ? charData.HairStyle[Rand.Next(0, hairStyleCount)]
                    : characterDefaultSettings.HairStyle;
                character.EyeCol = string.IsNullOrEmpty(characterDefaultSettings.EyeCol)
                    ? charData.EyeColor[Rand.Next(0, eyeColorCount)]
                    : characterDefaultSettings.EyeCol;
                character.Title = string.IsNullOrEmpty(characterDefaultSettings.Title)
                    ? null
                    : characterDefaultSettings.Title;
                character.BirthPlanet = universe.Planets?[Rand.Next(0, universe.Planets.Count)].Id;
                character.CurrentLocation = Rand.Next(0, 100) < 5
                    ? universe.Planets?[Rand.Next(0, universe.Planets.Count)].Id
                    : character.BirthPlanet;
                character.CrimeChance = characterDefaultSettings.CrimeChance == null ||
                                        characterDefaultSettings.CrimeChance.Length == 0 ||
                                        characterDefaultSettings.CrimeChance[0] == -1 ||
                                        characterDefaultSettings.CrimeChance[1] == -1
                    ? Rand.Next(1, 26) + Rand.Next(0, 25) + // Default chance up to 50%
                      (Rand.Next(0, 4) != 1
                          ? 0
                          : Rand.Next(1, 26) + Rand.Next(0, 25)) // 25% Additional crime roll chance
                    : Rand.Next(characterDefaultSettings.CrimeChance[0], characterDefaultSettings.CrimeChance[1] + 1);
                character.ShipId = string.IsNullOrEmpty(characterDefaultSettings.ShipId)
                    ? null
                    : characterDefaultSettings.ShipId;

                character.InitialReaction = (Rand.Next(0, 6) + Rand.Next(0, 6)) switch
                {
                    0 => charData.InitialReactions[0],
                    {} n when (n >= 1 && n <= 3) => charData.InitialReactions[1],
                    {} n when (n >= 4 && n <= 6) => charData.InitialReactions[2],
                    {} n when (n >= 7 && n <= 9) => charData.InitialReactions[3],
                    10 => charData.InitialReactions[4],
                    _ => charData.InitialReactions[2]
                };

                // Add the Character to the list of Characters in the universe
                universe.Characters.Add(character);

                cCount++;
            }

            // Re-order the list of Characters by their first name.
            universe.Characters = universe.Characters.OrderBy(c => c.First).ToList();

            return universe;
        }
    }
}