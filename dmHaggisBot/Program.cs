using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dmHaggisBot
{
    class Program
    {
        private static readonly string cwd = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\";

        private static readonly JObject world =
            JObject.Parse(
                File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\worldTags.json"));

        //Data out of the universe/person json
        private static string worldTags = (string) world.GetValue("WorldTags");


        static void Main(string[] args)
        {
            DiscordBot bot = new DiscordBot();
            bot.MainAsync();
            
            while (true)
            {
                
            }

            //Obsolete
            //SetDataFromReader setDataFromReader = new SetDataFromReader();

            // while (true)
            // {
            //     Console.Out.Write("Are you (C)reating or (L)oading a universe? ");
            //     var sel = Console.ReadLine();
            //
            //     if (sel == "")
            //         return;
            //
            //     if (sel.ToUpper() == "C")
            //     {
            //         Console.Out.Write("Enter a universe name ");
            //         Creation creation = new Creation(Console.ReadLine());
            //     }
            //
            //     if (sel.ToUpper() == "L")
            //     {
            //         Console.Out.Write("Enter a universe name ");
            //         Load load = new Load(Console.ReadLine());
            //     }
            // }
        }
    }
}