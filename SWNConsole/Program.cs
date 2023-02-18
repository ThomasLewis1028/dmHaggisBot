using System;
using System.Reflection;
using System.Threading.Tasks;
using SWNUniverseGenerator;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNConsole
{
    internal static class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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
                while (true)
                {
                    Console.Write("Enter universe name: ");
                    string universeName = Console.ReadLine();

                    Creation _creation = new Creation(UniversePath);

                    Universe _universe = _creation.CreateUniverse(new UniverseDefaultSettings());

                    _creation.CreateStars(_universe, new StarDefaultSettings());
                    _creation.CreatePlanets(_universe, new PlanetDefaultSettings());
                    _creation.CreateShips(_universe, new ShipDefaultSettings());
                    _creation.CreateCharacter(_universe, new CharacterDefaultSettings());

                    foreach (var character in _universe.Characters)
                    {   
                        Console.WriteLine($"{character.Id} - {character.Name}");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}