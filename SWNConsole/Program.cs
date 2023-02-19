using System.Reflection;
using SWNUniverseGenerator;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNConsole
{
    internal static class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private const string TerminalHeader = "swnconsole>";
        private static Creation? _creation;

        private static string UniversePath
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase!);
                var path = Uri.UnescapeDataString(uri.Path);
                if (!Directory.Exists(Path.GetDirectoryName(path) + "/UniverseFiles/"))
                    Directory.CreateDirectory(Path.GetDirectoryName(path) + "/UniverseFiles/");
                return Path.GetDirectoryName(path) + "/UniverseFiles/";
            }
        }

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
                        case var _ when RegularExpressions.LoadUniverseName.IsMatch(input):
                            if (RegularExpressions.LoadUniverse.IsMatch(input))
                                LoadUniverse();
                            else
                            {
                                string universeName = new string(input.SkipWhile(c => c != '"')
                                    .Skip(1)
                                    .TakeWhile(c => c != '"')
                                    .ToArray()).Trim();
                                LoadUniverse(universeName);
                            }

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
            _creation = new Creation(UniversePath);

            Universe universe = _creation.CreateUniverse(new UniverseDefaultSettings(universeName));

            _creation.CreateStars(universe, new StarDefaultSettings());
            _creation.CreatePlanets(universe, new PlanetDefaultSettings());
            _creation.CreateShips(universe, new ShipDefaultSettings());
            _creation.CreateCharacter(universe, new CharacterDefaultSettings());
        }

        private static void LoadUniverse()
        {
            Console.Clear();
            Console.Write("loaduniverse> ");
            Console.Write("Enter Universe Name: ");
            string universeName = Console.ReadLine();
            LoadUniverse(universeName);
        }

        private static void LoadUniverse(string universeName)
        {
        }
    }
}