using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace dmHaggisBot
{
    public class Creation
    {
        private static readonly string cwd = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\";

        public Creation(string name)
        {
            Directory.SetCurrentDirectory(cwd);
            StringBuilder path = new StringBuilder();
            path.Append(Directory.GetCurrentDirectory() + "\\" + name + ".json");
            if(File.Exists(path.ToString()))
            {
                Console.Out.Write("{0}.json already exists. Do you wish to overwrite? > ", name);
                string sel = Console.ReadLine();

                if (sel == "")
                    return;
                if (sel.ToUpper() == "Y")
                {
                    File.Delete(path.ToString());
                    Console.Out.WriteLine("{0} deleted\n", path);
                }
            }

            File.Create(path.ToString()).Close();

            Universe universe = new Universe();
            
            CharCreation charCreator = new CharCreation();
            StarCreation starCreator = new StarCreation();
            
            //new DiscordBot().MainAsync().GetAwaiter().GetResult();
            
            //Set up x/y grid and split the substring into X and Y values
            Console.Out.WriteLine("Insert your grid parameters (x y) > ");
            string xyIn = Console.ReadLine();

            int x = 15,
                y = 15;
            
            if (xyIn != "")
            {
                x = Int32.Parse(xyIn.Split(" ")[0]);
                y = Int32.Parse(xyIn.Split(" ")[1]);
            }

            Grid grid = new Grid(x, y);
            universe.Grid = grid;
            
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

                if (sel.ToUpper() == "C")
                    charCreator.Creation(universe.Characters);
                else if (sel.ToUpper() == "S")
                    starCreator.Creation(universe.Stars, grid);
                else if (sel.ToUpper() == "J")
                {}
            }
            
            using StreamWriter file =
                File.CreateText(path.ToString());
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, universe);
        }
    }
}