using System;
using System.Collections.Generic;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    /// <summary>
    /// This class holds the functions for creating Characters and adding them to the Universe.
    /// </summary>
    internal class CharCreation
    {
        private static readonly Random Rand = new();

        /// <summary>
        /// This function handles all Character creation. Should receive a Universe to edit and a set of
        /// CharacterDefaultSettings that will be used to set defaults.
        /// </summary>
        /// <param name="universeId"></param>
        /// <param name="characterDefaultSettings"></param>
        /// <param name="charData"></param>
        /// <param name="nameGenerations"></param>
        /// <returns>The newly modified universe</returns>
        public void AddCharacters(String universeId, CharacterDefaultSettings characterDefaultSettings)
        {
            using (var context = new UniverseContext())
            {
                using (var charRepo = new Repository<Character>(context))
                {
                    // Set the number of characters you want to create. Default is 100.
                    var count = characterDefaultSettings.Count < 0
                        ? 100
                        : characterDefaultSettings.Count;

                    List<Character> characters = new();

                    var cCount = 0;
                    while (cCount < count)
                    {
                        // Create the character with the specified first and last name
                        var character = new Character()
                        {
                            UniverseId = universeId
                        };

                        // Set sheet bounds including sheet# and row count.
                        // Must be done here to randomly select the sheet
                        var gender = characterDefaultSettings.Gender == Character.GenderEnum.Undefined
                            ? Rand.Next(0, 2)
                            : (int) characterDefaultSettings.Gender;

                        // var nameGeneration = gender == 0 ? nameGenerations[0] : nameGenerations[1];
                        // var lastNameGeneration = nameGenerations[2];

                        // Grab random values from their respective lists or use provided values
                        // First name
                        using (var repo = new Repository<Naming>(context))
                        {
                            character.First = string.IsNullOrEmpty(characterDefaultSettings.First)
                                ? character.Gender == Character.GenderEnum.Male
                                    ? ((Naming) repo.Random(n => n.NameType == "MaleName")).Name
                                    : ((Naming) repo.Random(n => n.NameType == "FemaleName")).Name
                                : characterDefaultSettings.First;
                        }

                        // Last name
                        using (var repo = new Repository<Naming>(context))
                        {
                            character.First = string.IsNullOrEmpty(characterDefaultSettings.First)
                                ? ((Naming) repo.Random(n => n.NameType == "LastName")).Name
                                : characterDefaultSettings.First;
                        }

                        // Character age
                        character.Age = characterDefaultSettings.Age == null ||
                                        characterDefaultSettings.Age.Length == 0 ||
                                        characterDefaultSettings.Age[0] == -1 || characterDefaultSettings.Age[1] == -1
                            // This creates Ages on a bell-curve where it's more likely to land somewhere around 30-45
                            // with a minimum of 15
                            ? Rand.Next(5, 22) + Rand.Next(5, 23) + Rand.Next(5, 23)
                            : Rand.Next(characterDefaultSettings.Age[0], characterDefaultSettings.Age[1]);
                        character.Gender = (Character.GenderEnum) gender;

                        using (var repo = new Repository<Naming>(context))
                        {
                            // Hair color
                            character.HairCol = string.IsNullOrEmpty(characterDefaultSettings.HairCol)
                                ? ((Naming) repo.Random(n => n.NameType == "HairColor")).Name
                                : characterDefaultSettings.HairCol;

                            // Hair style
                            character.HairStyle = string.IsNullOrEmpty(characterDefaultSettings.HairStyle)
                                ? ((Naming) repo.Random(n => n.NameType == "HairStyle")).Name
                                : characterDefaultSettings.HairStyle;

                            // Eye color
                            character.EyeCol = string.IsNullOrEmpty(characterDefaultSettings.EyeCol)
                                ? ((Naming) repo.Random(n => n.NameType == "EyeColor")).Name
                                : characterDefaultSettings.EyeCol;

                            // Skin color
                            // character.SkinCol = string.IsNullOrEmpty(characterDefaultSettings.SkinCol)
                            //     ? charData.SkinColor[Rand.Next(0, SkinColorCount)]
                            //     : characterDefaultSettings.SkinCol;
                            // Height
                            // character.Height = string.IsNullOrEmpty(characterDefaultSettings.Height)
                            //     ? Rand.Next(50, 200)
                            //     : Rand.Next(characterDefaultSettings.Height[0], characterDefaultSettings.Height[1]);
                        }

                        // Character title
                        character.Title = string.IsNullOrEmpty(characterDefaultSettings.Title)
                            ? null
                            : characterDefaultSettings.Title;

                        using (var repo = new Repository<Planet>(context))
                        {
                            // Character birth planet
                            character.BirthPlanetId = characterDefaultSettings.BirthPlanetId == null
                                ? repo.Random(p => p.UniverseId == universeId).Id
                                : characterDefaultSettings.BirthPlanetId;
                            
                            // Character current planet
                            character.CurrentLocationId = characterDefaultSettings.CurrentPlanetId == null
                                ? Rand.Next(0, 100) < 5
                                    ? repo.Random(p => p.UniverseId == universeId).Id
                                    : character.BirthPlanetId
                                : characterDefaultSettings.CurrentPlanetId;
                        }

                        // Character crime chance
                        character.CrimeChance = characterDefaultSettings.CrimeChance == null ||
                                                characterDefaultSettings.CrimeChance.Length == 0 ||
                                                characterDefaultSettings.CrimeChance[0] == -1 ||
                                                characterDefaultSettings.CrimeChance[1] == -1
                            ? Rand.Next(1, 26) + Rand.Next(0, 25) + // Default chance up to 50%
                              (Rand.Next(0, 4) != 1
                                  ? 0
                                  : Rand.Next(1, 26) + Rand.Next(0, 25)) // 25% Additional crime roll chance
                            : Rand.Next(characterDefaultSettings.CrimeChance[0],
                                characterDefaultSettings.CrimeChance[1] + 1);
                        
                        charRepo.Add(character);

                        // Character ship ID
                        if(!String.IsNullOrEmpty(characterDefaultSettings.ShipId))
                        {
                            using (var crewRepo = new Repository<CrewMember>(context))
                            {
                                var crewMember = new CrewMember()
                                {
                                    ShipId = characterDefaultSettings.ShipId,
                                    CharacterId = character.Id,
                                    UniverseId = universeId
                                };

                                crewRepo.Add(crewMember);
                            }
                        }

                        // // Character initial reaction towards players
                        // character.InitialReaction = (Rand.Next(0, 6) + Rand.Next(0, 6)) switch
                        // {
                        //     0 => charData.InitialReactions[0],
                        //     { } n when (n >= 1 && n <= 3) => charData.InitialReactions[1],
                        //     { } n when (n >= 4 && n <= 6) => charData.InitialReactions[2],
                        //     { } n when (n >= 7 && n <= 9) => charData.InitialReactions[3],
                        //     10 => charData.InitialReactions[4],
                        //     _ => charData.InitialReactions[2]
                        // };

                        using (var repo = new Repository<Naming>(context))
                            character.InitialReaction = ((Naming) repo.Random(n => n.NameType == "InitialReaction")).Name;
                        

                        // Add the Character to the list of Characters in the universe
                        characters.Add(character);

                        cCount++;
                    }

                    charRepo.UpdateRange(characters);
                    characters.Clear();
                }
            }
        }
    }
}