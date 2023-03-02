using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

/*
 * This is mostly obsolete. I used this function to copy/paste the descriptions from the Stars Without Number rulebook
 * so I could then transfer it to the worldTags.json file. This can be rewritten for any use.
 */

namespace SWNUniverseGenerator.OBSOLETE
{
    public class SetDataFromReader
    {
        public static void LoadNamesFromInput()
        {
            JObject starData =
                JObject.Parse(
                    File.ReadAllText(@"Data\StarData.json"));

            var starNames = starData.GetValue("Stars").ToList();
            var planetNames = starData.GetValue("Planets").ToList();

            var path = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\SWNUniverseGenerator\OBSOLETE\namesTemp.json";

            var names = new EntityNames();
            names.Stars = new List<string>();
            names.Planets = new List<string>();

            while (names.Planets.Count < 1000 && names.Stars.Count < 1000)
            {
                Console.Out.WriteLine("Enter Input: ");
                var input = Console.ReadLine();
                var input2 = Regex.Split(input, "^\\d+ ").ToList();

                foreach (var i in input2)
                {
                    if (string.IsNullOrEmpty(i)) continue;
                    names.Planets.Add(i);
                    names.Stars.Add(i);
                }
            }

            foreach (var i in starNames)
                names.Stars.Add(i.ToString());


            foreach (var i in planetNames)
                names.Planets.Add(i.ToString());

            names.Planets = names.Planets.Distinct().OrderBy(a => a.ToString()).ToList();
            names.Stars = names.Stars.Distinct().OrderBy(a => a.ToString()).ToList();

            using var file =
                File.CreateText(path);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, names);
        }

        public SetDataFromReader()
        {
            var path =
                @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\ReadFromDescriptions\worldTemp.json";
            var path2 = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\worldTags.json";

            var tags = new WorldTagList();

            var wTags =
                JObject.Parse(
                    File.ReadAllText(path2));

            foreach (var i in wTags["WorldTags"])
            {
                var tag = new WorldTag();

                using (var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
                {
                    Console.Out.WriteLine(" - " + i);
                    tag.Type = i.ToString().ToUpper();
                    Console.Out.WriteLine("Set Input: ");

                    //Allow for one long input with newlines and then split based on the bullets to the left
                    var input = sr.ReadToEnd();
                    var input2 = Regex.Split(input, "\n[E,F,C,T,P] ");

                    //Set the data to its proper place and replace all newlines, returns and ctrl+Z's
                    // tag.Description = input2[0].Replace("\n", " ").Replace("\r", "").Replace("\u001a", "");
                    // tag.Enemies = input2[1].Replace("\n", " ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                    // tag.Friends = input2[2].Replace("\n", " ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                    // tag.Complications =
                    //     input2[3].Replace("\n", " ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                    // tag.Things = input2[4].Replace("\n", " ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                    // tag.Places = input2[5].Replace("\n", " ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                }

                tags.WorldTags.Add(tag);
            }

            using var file =
                File.CreateText(path);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, tags);
        }
    }
}