using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    public class ProblemCreation
    {
        private static readonly Random rand = new Random();

        public Universe AddProblems(Universe universe, ProblemDefaultSettings problemDefaultSettings)
        {
            if (universe.Problems == null)
                universe.Problems = new List<Problem>();

            var probData = LoadProbData();

            var count = problemDefaultSettings.Count < 0
                ? 5
                : problemDefaultSettings.Count;

            var add = problemDefaultSettings.Additive;

            var id = string.IsNullOrEmpty(problemDefaultSettings.ID)
                ? null
                : problemDefaultSettings.ID;

            if (id != null)
            {

                var locID = (from planets in universe.Planets select new {planets.ID})
                    .Union(from stars in universe.Stars select new {stars.ID})
                    .Single(a => a.ID == id).ID;

                if (string.IsNullOrEmpty(locID))
                    throw new FileNotFoundException("No locations with ID " + id + " found");

                var probCount = 0;
                while (probCount < count)
                {
                    if (count <= universe.Problems.Count(a => a.LocationID == locID)
                        && !add)
                        break;

                    var problem = new Problem();

                    IDGen.GenerateID(problem);
                    problem.LocationID = locID;
                    var conflict = probData.Conflicts[rand.Next(0, probData.Conflicts.Count)];
                    problem.ConflictType = conflict.Type;
                    problem.Situation = conflict.Situations[rand.Next(0, 5)];
                    problem.Focus = conflict.Focuses[rand.Next(0, 5)];
                    problem.Restraint = probData.Restraints[rand.Next(0, 20)];
                    problem.Twist = probData.Twists[rand.Next(0, 20)];

                    universe.Problems.Add(problem);

                    probCount++;
                }
            }
            else
            {
                foreach (Planet planet in universe.Planets)
                {
                    var probCount = 0;
                    while (probCount < count)
                    {
                        if (probCount + count >= universe.Problems.Count(a => a.LocationID == planet.ID)
                            && !add)
                            break;

                        var problem = new Problem();

                        IDGen.GenerateID(problem);
                        problem.LocationID = planet.ID;
                        var conflict = probData.Conflicts[rand.Next(0, probData.Conflicts.Count)];
                        problem.ConflictType = conflict.Type;
                        problem.Situation = conflict.Situations[rand.Next(0, 5)];
                        problem.Focus = conflict.Focuses[rand.Next(0, 5)];
                        problem.Restraint = probData.Restraints[rand.Next(0, 20)];
                        problem.Twist = probData.Twists[rand.Next(0, 20)];

                        universe.Problems.Add(problem);

                        probCount++;
                    }
                }

                //TODO: Add more locations for problems
            }

            return universe;
        }

        private ProblemInfo LoadProbData()
        {
            var probData =
                JObject.Parse(
                    File.ReadAllText(@"Data\problemData.json"));

            return JsonConvert.DeserializeObject<ProblemInfo>(probData.ToString());
        }
    }
}