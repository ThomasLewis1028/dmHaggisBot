using System.Reflection;
using SWNUniverseGenerator;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNConsole
{
    internal static class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private const string TerminalHeader = "swnconsole>";
        private static Creation? _creation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private static async Task Main(string[] args)
        {
            try
            {
                Console.Clear();
                while (true)
                {
                    /*
                     * Options:
                     * 1. Load Universe - load
                     * 2. Create Universe - create
                     * 3. List Universes - list
                     * 4. Help - help
                     */
                    Console.Write($"{TerminalHeader} ");
                    string? input = Console.ReadLine();
                    switch (input)
                    {
                        case null:
                            Console.Clear();
                            Console.WriteLine("Please enter an input");
                            break;
                        // Create Universe
                        case var _ when RegularExpressions.CreateUniverseName.IsMatch(input):
                            if (RegularExpressions.CreateUniverse.IsMatch(input))
                                CreateUniverse();
                            else
                            {
                                string universeName = new string(input.SkipWhile(c => c != '"')
                                    .Skip(1)
                                    .TakeWhile(c => c != '"')
                                    .ToArray()).Trim();
                                CreateUniverse(universeName);
                            }

                            break;
                        // Load Universe
                        // case var _ when RegularExpressions.LoadUniverseName.IsMatch(input):
                        //     if (RegularExpressions.LoadUniverse.IsMatch(input))
                        //         LoadUniverse();
                        //     else
                        //     {
                        //         string universeName = new string(input.SkipWhile(c => c != '"')
                        //             .Skip(1)
                        //             .TakeWhile(c => c != '"')
                        //             .ToArray()).Trim();
                        //         LoadUniverse(universeName);
                        //     }

                            break;
                        // List commands
                        case var _ when RegularExpressions.GetHelp.IsMatch(input):
                            Console.Clear();
                            Console.WriteLine(
                                "Help: \nload\tLoad Universe \ncreate\tCreate Universe \nhelp\tList Help");
                            break;
                        // List existing universe files
                        case var _ when RegularExpressions.ListUniverses.IsMatch(input):

                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Please enter a valid input");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private static void CreateUniverse()
        {
            Console.Clear();
            Console.Write("createuniverse> ");
            Console.Write("Enter Universe Name: ");
            string universeName = Console.ReadLine();
            CreateUniverse(universeName);
        }

        private static void CreateUniverse(string universeName)
        {
            using (var context = new UniverseContext())
            {
                string universeId = "";
                using (var uc = new Repository<Universe>(context))
                {
                    //Add Universe
                    Universe universe = new Universe()
                    {
                        Name = universeName,
                        GridX = 8,
                        GridY = 10
                    };
                    uc.Add(universe);
                }
            }
            //_creation = new Creation();

            // Universe universe = _creation.CreateUniverse(new UniverseDefaultSettings{Name = universeName, Overwrite = true});
            //
            // _creation.CreateStars(universe, new StarDefaultSettings());
            // _creation.CreatePlanets(universe, new PlanetDefaultSettings());
            // _creation.CreateShips(universe, new ShipDefaultSettings());
            // _creation.CreateCharacter(universe, new CharacterDefaultSettings());
        }

        /*private static void LoadUniverse()
        {
            Console.Clear();
            Console.Write("loaduniverse> ");
            Console.Write("Enter Universe Name: ");
            string universeName = Console.ReadLine();
            LoadUniverse(universeName);
        }

        private static void LoadUniverse(string universeName)
        {
            _creation = new Creation();

            Universe universe = _creation.LoadUniverse(universeName);
            
            Console.Write($"{universe.Name}> \n");
            
            Console.WriteLine($"{universe.Characters.First().Name}\n{universe.Characters.First().Id}");
            
        }*/
    }
}