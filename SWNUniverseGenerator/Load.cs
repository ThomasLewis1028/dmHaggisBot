using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SWNUniverseGenerator
{
    public class Load
    {
        private static readonly string cwd = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\";

        public Load(string name)
        {
            Directory.SetCurrentDirectory(cwd);
            StringBuilder path = new StringBuilder();
            path.Append(Directory.GetCurrentDirectory() + "\\" + name + ".json");
            if (!File.Exists(path.ToString()))
            {
                Console.Out.Write("{0}.json doesn't exist. Do you wish to create one? > ", name);
                string sel = Console.ReadLine();

                if (sel == "")
                    return;
                if (sel.ToUpper() == "Y")
                {
                    File.Create(path.ToString()).Close();
                }
            }


            JObject univ =
                JObject.Parse(
                    File.ReadAllText(path.ToString()));

            Universe universe = JsonConvert.DeserializeObject<Universe>(univ.ToString());

            CharCreation charCreator = new CharCreation();
            StarCreation starCreator = new StarCreation();

            //new DiscordBot().MainAsync().GetAwaiter().GetResult();

            while (true)
            {
                Console.Write("Would you like to create a (C)haracter, (S)ystem, or (J)ob? > ");
                string sel = Console.ReadLine();

                //Check 
                if (sel == "")
                    break;

                /* USE LATER - FROM METHOD
                var options =  {'c': self.character, 'j': self.job}
                options[sel]()
                */

                // if (sel.ToUpper() == "C")
                //     charCreator.Creation(universe.Characters);
                // else if (sel.ToUpper() == "S")
                //     starCreator.Creation(universe.Stars, universe.Grid);
                // else if (sel.ToUpper() == "J")
                // {
                // }
            }

            using StreamWriter file =
                File.CreateText(path.ToString());
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, universe);
        }
    }
}