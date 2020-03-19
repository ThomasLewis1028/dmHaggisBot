using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dmHaggisBot
{
    public class StarCreation
    {
        private static Random rand = new Random();

        private static readonly string cwd = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\";
        
        //Properties file
        private static readonly JObject prop =
            JObject.Parse(
                File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\properties.json"));
        
        //WorldTags file
        // private static readonly JObject worldTags =
        //     JObject.Parse(
        //         File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\worldTags.json"));

        //Data out of the universe/person json
        private static string starData = (string) prop.GetValue("starData");

        public void AddStar(Universe universe, StarDefaultSettings starDefaultSettings)
        {
            Excel starExcel = new Excel(starData);
            var starReader = starExcel.ReaderReturn(starData);

            var path = cwd + "\\worldTags.json";
            JObject tags =
                JObject.Parse(
                    File.ReadAllText(path.ToString()));

            WorldInfo worldInfo = JsonConvert.DeserializeObject<WorldInfo>(tags.ToString());
            
            Console.Out.Write("How many Systems would you like to create? ");
            int sc = Int32.Parse(Console.ReadLine());
            Console.Out.Write("What is the maximum number of planets per system? ");
            int pc = Int32.Parse(Console.ReadLine());
            
            //Set sheet bounds inluding sheet# and row count.
            var starLen = starReader.Tables[0].Rows.Count;
            var planLen = starReader.Tables[1].Rows.Count;

            int sCount = 0;
            while (sCount < sc)
            {
                Star star = new Star(
                    starReader.Tables[0].Rows[rand.Next(0, starLen - 1)].ItemArray[0].ToString(),
                    rand.Next(0, universe.Grid.X+1), rand.Next(0, universe.Grid.Y + 1));

                int pCount = 0;
                int pMax = rand.Next(0, pc + 1);
                while (pCount < pMax)
                {
                    Planet planet =
                        new Planet(starReader.Tables[1].Rows[rand.Next(0, planLen - 1)].ItemArray[0].ToString());
                    planet.WorldTag = worldInfo.WorldTags[rand.Next(0, 100)];
                    planet.Atmosphere = worldInfo.Atmospheres[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Temperature = worldInfo.Temperatures[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Biosphere = worldInfo.Biospheres[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Population = worldInfo.Populations[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.TechLevel = worldInfo.TechLevels[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Origin = worldInfo.OWOrigins[rand.Next(0,8)];
                    planet.Relationship = worldInfo.OWRelationships[rand.Next(0, 8)];
                    planet.Contact = worldInfo.OWContacts[rand.Next(0, 8)];
                    

                    star.Planets.Add(planet);
                    pCount++;
                }

                sCount++;

                Console.Out.WriteLine("{0} - ({1}, {2})", star.Name, star.X, star.Y);
                foreach (var p in star.Planets)
                {
                    Console.Out.WriteLine("\t" + p.Name);
                }
            }
        }
    }
}