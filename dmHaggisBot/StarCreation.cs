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

        //Properties file
        private static readonly JObject prop =
            JObject.Parse(
                File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\properties.json"));

        //Data out of the universe/person json
        private static string starData = (string) prop.GetValue("starData");

        public List<Star> Creation(List<Star> stars, Grid grid)
        {
            Excel starExcel = new Excel(starData);
            var starReader = starExcel.ReaderReturn(starData);

            Console.Out.Write("How many Systems would you like to create? > ");
            int sc = Int32.Parse(Console.ReadLine());
            Console.Out.Write("What is the maximum number of planets per system? > ");
            int pc = Int32.Parse(Console.ReadLine());

            //Set sheet bounds inluding sheet# and row count.
            var starLen = starReader.Tables[0].Rows.Count;
            var planLen = starReader.Tables[1].Rows.Count;

            int sCount = 0;
            while (sCount < sc)
            {
                Star star = new Star(
                    starReader.Tables[0].Rows[rand.Next(0, starLen - 1)].ItemArray[0].ToString(),
                    rand.Next(0, grid.X + 1), rand.Next(0, grid.Y + 1));

                int pCount = 0;
                int pMax = rand.Next(0, pc + 1);
                while (pCount < pMax)
                {
                    Planet planet =
                        new Planet(starReader.Tables[1].Rows[rand.Next(0, planLen - 1)].ItemArray[0].ToString());

                    star.Planets.Add(planet);
                    pCount++;
                }

                sCount++;

                Console.Out.WriteLine("{0} - ({1}, {2})", star.Name, star.X, star.Y);
                foreach (var p in star.Planets)
                {
                    Console.Out.WriteLine("\t" + p.Name);
                }

                stars.Add(star);
            }

            return stars;
        }
    }
}