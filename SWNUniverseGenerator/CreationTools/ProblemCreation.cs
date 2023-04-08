using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    internal class ProblemCreation
    {
        private static readonly Random Rand = new Random();

        public void AddProblems(string universeId, ProblemDefaultSettings problemDefaultSettings)
        {
            // if (universeId.Problems == null)
            //     universeId.Problems = new List<Problem>();
            //
            // var count = problemDefaultSettings.Count < 0
            //     ? 5
            //     : problemDefaultSettings.Count;
            //
            // var add = problemDefaultSettings.Additive;
            //
            // var id = string.IsNullOrEmpty(problemDefaultSettings.Id)
            //     ? null
            //     : problemDefaultSettings.Id;
            //
            // if (id != null)
            // {
            //     var locId = (from planets in universeId.Planets select new {ID = planets.Id})
            //         .Union(from stars in universeId.Stars select new {ID = stars.Id})
            //         .Single(a => a.ID == id).ID;
            //
            //     if (string.IsNullOrEmpty(locId))
            //         throw new FileNotFoundException("No locations with ID " + id + " found");
            //
            //     var probCount = 0;
            //     while (probCount < count)
            //     {
            //         if (count <= universeId.Problems.Count(a => a.LocationId == locId)
            //             && !add)
            //             break;
            //
            //         var problem = new Problem();
            //
            //         problem.LocationId = locId;
            //         var conflict = problemData.Conflicts[Rand.Next(0, problemData.Conflicts.Count)];
            //         problem.ConflictType = conflict.Type;
            //         problem.Situation = conflict.Situations[Rand.Next(0, 5)];
            //         problem.Focus = conflict.Focuses[Rand.Next(0, 5)];
            //         problem.Restraint = problemData.Restraints[Rand.Next(0, 20)];
            //         problem.Twist = problemData.Twists[Rand.Next(0, 20)];
            //
            //         universeId.Problems.Add(problem);
            //
            //         probCount++;
            //     }
            // }
            // else
            // {
            //     foreach (Planet planet in universeId.Planets)
            //     {
            //         var probCount = 0;
            //         while (probCount < count)
            //         {
            //             if (probCount + count <= universeId.Problems.Count(a => a.LocationId == planet.Id)
            //                 && !add)
            //                 break;
            //
            //             var problem = new Problem();
            //
            //             problem.LocationId = planet.Id;
            //             var conflict = problemData.Conflicts[Rand.Next(0, problemData.Conflicts.Count)];
            //             problem.ConflictType = conflict.Type;
            //             problem.Situation = conflict.Situations[Rand.Next(0, 5)];
            //             problem.Focus = conflict.Focuses[Rand.Next(0, 5)];
            //             problem.Restraint = problemData.Restraints[Rand.Next(0, 20)];
            //             problem.Twist = problemData.Twists[Rand.Next(0, 20)];
            //
            //             universeId.Problems.Add(problem);
            //
            //             probCount++;
            //         }
            //     }
            //
            //     //TODO: Add more locations for problems
            // }
        }
    }
}