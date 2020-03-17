using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Json;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dmHaggisBot
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            // DiscordBot bot = new DiscordBot();
            // bot.MainAsync();
            //
            // while (true)
            // {
            //     
            // }
            
            while (true)
            {
                Console.Out.Write("Are you (C)reating or (L)oading a universe? > ");
                var sel = Console.ReadLine();

                if (sel == "")
                    return;

                if (sel.ToUpper() == "C")
                {
                    Console.Out.Write("Enter a universe name > ");
                    Creation creation = new Creation(Console.ReadLine());
                }

                if (sel.ToUpper() == "L")
                {
                    Console.Out.Write("Enter a universe name > ");
                    Load load = new Load(Console.ReadLine());
                }
            }
        }
    }
}