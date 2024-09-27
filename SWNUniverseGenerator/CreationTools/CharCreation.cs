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
        /// <param name="cds"></param>
        public void AddCharacters(CharacterDefaultSettings cds)
        {
            var maleNameGens = new NameGeneration();
            var femaleNameGens = new NameGeneration();
            var lastNameGens = new NameGeneration();

            if (cds.Balanced.Balanced &&
                !string.IsNullOrEmpty(cds.BirthPlanetId))
                throw new Exception("Cannot combine balanced character creation with a specified planet ID");
            //
            // if (characterDefaultSettings.Balanced.Balanced && characterDefaultSettings.Count > 0)
            //     throw new Exception("Cannot combine balanced character creation with a specified number of characters");


            using (var context = new UniverseContext())
            {
                // using (var nameRepo = new Repository<Naming>(context))
                // {
                //     maleNameGenerations.GenerateChain(nameRepo
                //         .Search(n => n.NameType == "MaleName")
                //         .Cast<Naming>()
                //         .ToList());
                //     femaleNameGenerations.GenerateChain(nameRepo
                //         .Search(n => n.NameType == "FemaleName")
                //         .Cast<Naming>()
                //         .ToList());
                //     lastNameGenerations.GenerateChain(nameRepo
                //         .Search(n => n.NameType == "LastName")
                //         .Cast<Naming>()
                //         .ToList());
                // }

                using (var charRepo = new Repository<Character>(context))
                {
                    List<Character> characters = new();

                    if (cds.Balanced.Balanced)
                    {
                        using (var planetRepo = new Repository<Planet>(context))
                        {
                            var totalPop = planetRepo.Search(e => e.UniverseId == cds.UniverseId).Cast<Planet>().Sum(e => e.Population);
                            var percentage = (double)cds.Count / totalPop;

                            foreach (var planet in planetRepo.Search(e => e.UniverseId == cds.UniverseId).Cast<Planet>())
                            {
                                var cCount = 0;
                                var max = planet.Population * percentage > 10
                                    ? (int)(planet.Population * percentage)
                                    : 10;

                                while (cCount < max)
                                {
                                    cds.BirthPlanetId = planet.Id;
                                    cCount = CreateCharacter(cds, context, maleNameGens,
                                        femaleNameGens, lastNameGens, charRepo, characters, cCount);

                                    if (cCount % 100 != 0) continue;
                                    charRepo.UpdateRange(characters);
                                    characters.Clear();
                                }
                            }
                        }
                    }
                    else
                    {
                        var cCount = 0;
                        while (cCount < cds.Count)
                        {
                            cCount = CreateCharacter(cds, context, maleNameGens,
                                femaleNameGens, lastNameGens, charRepo, characters, cCount);
                            
                            if (cCount % 100 == 0)
                            {
                                charRepo.UpdateRange(characters);
                                characters.Clear();
                            }
                        }
                    }

                    charRepo.UpdateRange(characters);
                    characters.Clear();
                }
            }
        }

        private static int CreateCharacter(CharacterDefaultSettings cds, UniverseContext context,
            NameGeneration maleNameGens, NameGeneration femaleNameGens,
            NameGeneration lastNameGens,
            Repository<Character> charRepo, List<Character> characters, int cCount)
        {
            // Create the character with the specified first and last name
            var character = new Character()
            {
                UniverseId = cds.UniverseId
            };

            // Set sheet bounds including sheet# and row count.
            // Must be done here to randomly select the sheet
            var gender = cds.Gender == Character.GenderEnum.Undefined
                ? Rand.Next(0, 2)
                : (int)cds.Gender;

            // var nameGeneration = gender == 0 ? nameGenerations[0] : nameGenerations[1];
            // var lastNameGeneration = nameGenerations[2];

            // Grab random values from their respective lists or use provided values
            // First name
            using (var repo = new Repository<Naming>(context))
            {
                if (string.IsNullOrEmpty(cds.First))
                {
                    var nameRand = Rand.Next(0, 10);
                    nameRand = 4;

                    if (nameRand > 3)
                    {
                        character.First = character.Gender == Character.GenderEnum.Male
                            ? ((Naming)repo.Random(n => n.NameType == "MaleName")).Name
                            : ((Naming)repo.Random(n => n.NameType == "FemaleName")).Name;
                    }
                    else
                    {
                        character.First = character.Gender == Character.GenderEnum.Male
                            ? maleNameGens.GenerateName()
                            : femaleNameGens.GenerateName();
                    }
                }
                else
                    character.First = cds.First;
            }

            // Last name
            using (var repo = new Repository<Naming>(context))
            {
                var nameRand = Rand.Next(0, 10);

                if (string.IsNullOrEmpty(cds.Last))
                {
                    nameRand = 4;
                    if (nameRand > 3)
                        character.Last = ((Naming)repo.Random(n => n.NameType == "LastName")).Name;
                    else
                        character.Last = lastNameGens.GenerateName();
                }
                else
                    character.Last = cds.Last;
            }

            // Character age
            character.Age = cds.Age < 0
                ? cds.AgeRange == null ||
                  cds.AgeRange.Length == 0 ||
                  cds.AgeRange[0] == -1 || cds.AgeRange[1] == -1
                    // This creates Ages on a bell-curve where it's more likely to land somewhere around 30-45
                    // with a minimum of 15
                    ? Rand.Next(5, 22) + Rand.Next(5, 23) + Rand.Next(5, 23)
                    : Rand.Next(cds.AgeRange[0], cds.AgeRange[1])
                : cds.Age;
            character.Gender = (Character.GenderEnum)gender;

            using (var repo = new Repository<Naming>(context))
            {
                // Hair color
                character.HairCol = string.IsNullOrEmpty(cds.HairCol)
                    ? ((Naming)repo.Random(n => n.NameType == "HairColor")).Name
                    : cds.HairCol;

                // Hairstyle
                character.HairStyle = string.IsNullOrEmpty(cds.HairStyle)
                    ? ((Naming)repo.Random(n => n.NameType == "HairStyle")).Name
                    : cds.HairStyle;

                // Eye color
                character.EyeCol = string.IsNullOrEmpty(cds.EyeCol)
                    ? ((Naming)repo.Random(n => n.NameType == "EyeColor")).Name
                    : cds.EyeCol;

                // Skin color
                character.SkinCol = string.IsNullOrEmpty(cds.SkinCol)
                    ? null
                    : cds.SkinCol;
                // Height
                character.Height = cds.Height < 0
                        ? Rand.Next(150, 200)
                        : cds.Height
                    //Rand.Next(characterDefaultSettings.Height[0], characterDefaultSettings.Height[1])
                    ;
            }

            // Character title
            character.Title = string.IsNullOrEmpty(cds.Title)
                ? null
                : cds.Title;

            using (var repo = new Repository<Planet>(context))
            {
                // Character birth planet
                character.BirthPlanetId = cds.BirthPlanetId
                                          ?? repo.Random(p =>
                                              p.UniverseId == cds.UniverseId).Id;

                // Character current planet
                character.CurrentLocationId = cds.CurrentPlanetId
                                              ?? (Rand.Next(0, 100) < 5
                                                  ? repo.Random(p =>
                                                          p.UniverseId == cds
                                                              .UniverseId)
                                                      .Id
                                                  : character.BirthPlanetId);
            }

            // Character crime chance
            character.CrimeChance = cds.CrimeChance == null ||
                                    cds.CrimeChance.Length == 0 ||
                                    cds.CrimeChance[0] == -1 ||
                                    cds.CrimeChance[1] == -1
                ? Rand.Next(1, 26) + Rand.Next(0, 25) + // Default chance up to 50%
                  (Rand.Next(0, 4) != 1
                      ? 0
                      : Rand.Next(1, 26) + Rand.Next(0, 25)) // 25% Additional crime roll chance
                : Rand.Next(cds.CrimeChance[0],
                    cds.CrimeChance[1] + 1);

            charRepo.Add(character);

            // Character ship ID
            if (!String.IsNullOrEmpty(cds.ShipId))
            {
                using (var crewRepo = new Repository<CrewMember>(context))
                {
                    var crewMember = new CrewMember()
                    {
                        ShipId = cds.ShipId,
                        CharacterId = character.Id,
                        UniverseId = cds.UniverseId
                    };

                    crewRepo.Add(crewMember);
                }
            }

            // Character initial reaction towards players


            using (var repo = new Repository<Naming>(context))
            {
                character.InitialReaction = string.IsNullOrEmpty(cds.InitialReaction)
                    ? character.InitialReaction =
                        ((Naming)repo.Random(n => n.NameType == "InitialReaction")).Name
                    : cds.InitialReaction;
            }


            // Add the Character to the list of Characters in the universe
            characters.Add(character);

            cCount++;
            return cCount;
        }
    }
}