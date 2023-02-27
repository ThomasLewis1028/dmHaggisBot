using System;
using System.Collections.Generic;
using System.Linq;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    internal class AlienCreation
    {
        private static readonly Random Rand = new Random();

        public Universe AddAliens(Universe universe, AlienDefaultSettings alienDefaultSettings, AlienData alienData)
        {
            // If no Characters have been created on the Universe then give it an empty list of them.
            // universe.Aliens ??= new List<Alien>();
            //
            // var count = alienDefaultSettings.Count < 0
            //     ? Rand.Next(0, universe.Planets.Count)
            //     : alienDefaultSettings.Count;
            //
            // var bCount = alienDefaultSettings.BodyTraitCount < 0
            //     ? -1
            //     : alienDefaultSettings.BodyTraitCount;
            //
            // //Generate an Alien
            // var aCount = 0;
            // while (aCount < count)
            // {
            //     var alien = new Alien();
            //
            //     // if (universe.Aliens.Exists(a => a.Id == alien.Id))
            //     //     continue;
            //
            //     alien.BodyTraits = "";
            //
            //     // Alien Body Traits
            //     if (bCount > 0)
            //     {
            //         var bCounter = 0;
            //
            //         while (bCounter < bCount)
            //         {
            //             var trait = alienData.BodyTraits[Rand.Next(0, alienData.BodyTraits.Count)];
            //             if (!alien.BodyTraits.Contains(trait))
            //                 alien.BodyTraits += (trait + "|");
            //             else continue;
            //
            //             bCounter++;
            //         }
            //     }
            //     else // If there were no traits passed in, pick random
            //     {
            //         while (true)
            //         {
            //             var trait = alienData.BodyTraits[Rand.Next(0, alienData.BodyTraits.Count)];
            //             if (!alien.BodyTraits.Contains(trait))
            //                 alien.BodyTraits += (trait + "|");
            //             else continue;
            //
            //             if (Rand.Next(0, 6) == 0)
            //                 continue;
            //             break;
            //         }
            //     }
            //
            //     alien.Lenses = "";
            //     // Alien Lenses
            //     if (alienDefaultSettings.Lenses == null || alienDefaultSettings.Lenses != null)
            //     {
            //         var lCount = Rand.Next(0, 2) + 1;
            //         var lCounter = 0;
            //
            //         while (lCounter < lCount)
            //         {
            //             var lens = alienData.Lenses[Rand.Next(0, alienData.Lenses.Count)].Type;
            //             if (!alien.Lenses.Contains(lens))
            //                 alien.Lenses += (lens + "|");
            //             else continue;
            //
            //             lCounter++;
            //         }
            //     }
            //     else
            //         alien.Lenses = alienDefaultSettings.Lenses;
            //
            //     alien.SocialStructures = "";
            //     // Alien Social Structure
            //     if (Rand.Next(0, 2) == 0)
            //         alien.SocialStructures += (alienData
            //             .SocialStructures[Rand.Next(0, alienData.SocialStructures.Count)].Type) + "|";
            //     else
            //     {
            //         var sCounter = 0;
            //         var sCount = Rand.Next(0, 3) + 1;
            //         alien.MultiPolarType = Rand.Next(0, 2) == 0 ? "Cooperative" : "Competitive";
            //         while (sCounter < sCount)
            //         {
            //             var type = alienData.SocialStructures[Rand.Next(0, alienData.SocialStructures.Count)].Type;
            //             if (!alien.SocialStructures.Contains(type))
            //                 alien.SocialStructures += (type + "|");
            //             else continue;
            //
            //             sCounter++;
            //         }
            //     }
            //
            //     universe.Aliens.Add(alien);
            //
            //     aCount++;
            // }
            //
            // universe.Aliens = universe.Aliens.OrderBy(a => a.Id).ToList();

            return universe;
        }
    }
}