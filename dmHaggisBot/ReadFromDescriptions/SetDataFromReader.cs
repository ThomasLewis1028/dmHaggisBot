using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/*
 * This is mostly obsolete. I used this function to copy/paste the descriptions from the Stars Without Number rulebook
 * so I could then transfer it to the worldTags.json file. This can be rewritten for any use.
 */

namespace dmHaggisBot
{
    public class SetDataFromReader
    {
        //Set temporary file that has my planet types
        private static readonly JObject world =
            JObject.Parse(
                File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\ReadFromDescriptions\worldTags.json"));

        //Data out of the universe/person json
        private static string worldTags = (string) world.GetValue("WorldTags");


        public SetDataFromReader()
        {
            var path = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\ReadFromDescriptions\worldTemp.json";
            var path2 = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\worldTags.json";

            WorldTagList tags = new WorldTagList();

            JObject wTags =
                JObject.Parse(
                    File.ReadAllText(path2));

            foreach (var i in wTags["WorldTags"])
            {
                WorldTag tag = new WorldTag();
                
                using (var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
                {
                    Console.Out.WriteLine(" - " + i);
                    tag.Type = i.ToString().ToUpper();
                    Console.Out.WriteLine("Set Input: ");

                    //Allow for one long input with newlines and then split based on the bullets to the left
                    var input = sr.ReadToEnd();
                    var input2 = Regex.Split(input, "\n[E,F,C,T,P] ");
                    
                    //Set the data to its proper place and replace all newlines, returns and ctrl+Z's
                    tag.Description = input2[0].Replace("\n"," ").Replace("\r", "").Replace("\u001a", "");
                    tag.Enemies = input2[1].Replace("\n"," ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                    tag.Friends = input2[2].Replace("\n"," ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                    tag.Complications = input2[3].Replace("\n"," ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                    tag.Things = input2[4].Replace("\n"," ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                    tag.Places = input2[5].Replace("\n"," ").Replace("\r", "").Replace("\u001a", "").Split(", ");
                }
                tags.WorldTags.Add(tag);
            }
            
            using StreamWriter file =
                File.CreateText(path);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, tags);
        }
    }
}