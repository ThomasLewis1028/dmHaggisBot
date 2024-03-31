using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="characterDefaultSettings"></param>
        public void AddCharacters(CharacterDefaultSettings characterDefaultSettings)
        {
            var maleNameGenerations = new NameGeneration();
            var femaleNameGenerations = new NameGeneration();
            var lastNameGenerations = new NameGeneration();

            using (var context = new UniverseContext())
            {
                using (var nameRepo = new Repository<Naming>(context))
                {
                    maleNameGenerations.GenerateChain(nameRepo
                        .Search(n => n.NameType == "MaleName")
                        .Cast<Naming>()
                        .ToList());
                    femaleNameGenerations.GenerateChain(nameRepo
                        .Search(n => n.NameType == "FemaleName")
                        .Cast<Naming>()
                        .ToList());
                    lastNameGenerations.GenerateChain(nameRepo
                        .Search(n => n.NameType == "LastName")
                        .Cast<Naming>()
                        .ToList());
                }

                using (var charRepo = new Repository<Character>(context))
                {
                    List<Character> characters = new();

                    var cCount = 0;
                    while (cCount < characterDefaultSettings.Count)
                    {
                        // Create the character with the specified first and last name
                        var character = new Character()
                        {
                            UniverseId = characterDefaultSettings.UniverseId
                        };

                        // Set sheet bounds including sheet# and row count.
                        // Must be done here to randomly select the sheet
                        var gender = characterDefaultSettings.Gender == Character.GenderEnum.Undefined
                            ? Rand.Next(0, 2)
                            : (int)characterDefaultSettings.Gender;

                        // var nameGeneration = gender == 0 ? nameGenerations[0] : nameGenerations[1];
                        // var lastNameGeneration = nameGenerations[2];

                        // Grab random values from their respective lists or use provided values
                        // First name
                        using (var repo = new Repository<Naming>(context))
                        {
                            if (string.IsNullOrEmpty(characterDefaultSettings.First))
                            {
                                var nameRand = Rand.Next(0, 10);

                                if (nameRand > 3)
                                {
                                    character.First = character.Gender == Character.GenderEnum.Male
                                        ? ((Naming)repo.Random(n => n.NameType == "MaleName")).Name
                                        : ((Naming)repo.Random(n => n.NameType == "FemaleName")).Name;
                                }
                                else
                                {
                                    character.First = character.Gender == Character.GenderEnum.Male
                                        ? maleNameGenerations.GenerateName()
                                        : femaleNameGenerations.GenerateName();
                                }
                            }
                            else
                                character.First = characterDefaultSettings.First;
                        }

                        // Last name
                        using (var repo = new Repository<Naming>(context))
                        {
                            var nameRand = Rand.Next(0, 10);

                            if (string.IsNullOrEmpty(characterDefaultSettings.Last))
                            {
                                if (nameRand > 3)
                                    character.Last = ((Naming)repo.Random(n => n.NameType == "LastName")).Name;
                                else
                                    character.Last = lastNameGenerations.GenerateName();
                            }
                            else
                                character.Last = characterDefaultSettings.Last;
                        }

                        // Character age
                        character.Age = characterDefaultSettings.Age < 0
                            ? characterDefaultSettings.AgeRange == null ||
                              characterDefaultSettings.AgeRange.Length == 0 ||
                              characterDefaultSettings.AgeRange[0] == -1 || characterDefaultSettings.AgeRange[1] == -1
                                // This creates Ages on a bell-curve where it's more likely to land somewhere around 30-45
                                // with a minimum of 15
                                ? Rand.Next(5, 22) + Rand.Next(5, 23) + Rand.Next(5, 23)
                                : Rand.Next(characterDefaultSettings.AgeRange[0], characterDefaultSettings.AgeRange[1])
                            : characterDefaultSettings.Age;
                        character.Gender = (Character.GenderEnum)gender;

                        using (var repo = new Repository<Naming>(context))
                        {
                            // Hair color
                            character.HairCol = string.IsNullOrEmpty(characterDefaultSettings.HairCol)
                                ? ((Naming)repo.Random(n => n.NameType == "HairColor")).Name
                                : characterDefaultSettings.HairCol;

                            // Hair style
                            character.HairStyle = string.IsNullOrEmpty(characterDefaultSettings.HairStyle)
                                ? ((Naming)repo.Random(n => n.NameType == "HairStyle")).Name
                                : characterDefaultSettings.HairStyle;

                            // Eye color
                            character.EyeCol = string.IsNullOrEmpty(characterDefaultSettings.EyeCol)
                                ? ((Naming)repo.Random(n => n.NameType == "EyeColor")).Name
                                : characterDefaultSettings.EyeCol;

                            // Skin color
                            character.SkinCol = string.IsNullOrEmpty(characterDefaultSettings.SkinCol)
                                ? null
                                : characterDefaultSettings.SkinCol;
                            // Height
                            character.Height = characterDefaultSettings.Height < 0
                                    ? Rand.Next(150, 200)
                                    : characterDefaultSettings.Height
                                //Rand.Next(characterDefaultSettings.Height[0], characterDefaultSettings.Height[1])
                                ;
                        }

                        // Character title
                        character.Title = string.IsNullOrEmpty(characterDefaultSettings.Title)
                            ? null
                            : characterDefaultSettings.Title;

                        using (var repo = new Repository<Planet>(context))
                        {
                            // Character birth planet
                            character.BirthPlanetId = characterDefaultSettings.BirthPlanetId
                                                      ?? repo.Random(p =>
                                                          p.UniverseId == characterDefaultSettings.UniverseId).Id;

                            // Character current planet
                            character.CurrentLocationId = characterDefaultSettings.CurrentPlanetId
                                                          ?? (Rand.Next(0, 100) < 5
                                                              ? repo.Random(p =>
                                                                      p.UniverseId == characterDefaultSettings
                                                                          .UniverseId)
                                                                  .Id
                                                              : character.BirthPlanetId);
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
                        if (!String.IsNullOrEmpty(characterDefaultSettings.ShipId))
                        {
                            using (var crewRepo = new Repository<CrewMember>(context))
                            {
                                var crewMember = new CrewMember()
                                {
                                    ShipId = characterDefaultSettings.ShipId,
                                    CharacterId = character.Id,
                                    UniverseId = characterDefaultSettings.UniverseId
                                };

                                crewRepo.Add(crewMember);
                            }
                        }

                        // Character initial reaction towards players


                        using (var repo = new Repository<Naming>(context))
                        {
                            character.InitialReaction = string.IsNullOrEmpty(characterDefaultSettings.InitialReaction)
                                ? character.InitialReaction =
                                    ((Naming)repo.Random(n => n.NameType == "InitialReaction")).Name
                                : characterDefaultSettings.InitialReaction;
                        }


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