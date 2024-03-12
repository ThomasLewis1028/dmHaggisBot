using System.Text.RegularExpressions;

namespace dmHaggisBot
{
    /// <summary>
    /// This class contains a list of my regular expressions to be used in DiscordBot.cs
    ///
    /// This was created to clean up the bot file
    /// </summary>
    public class RegularExpressions
    {
        public static readonly Regex CharacterCreate =
            new Regex("^(charCreate|createChar|cc)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex StarCreate =
            new Regex("^(starCreate|createStar|sc|cs)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex PlanetCreate =
            new Regex("^(planetCreate|createPlanet|pc|cp)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex UniverseCreate =
            new Regex("^(univCreate|createUniv|uc|cu)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex UniverseLoad = 
            new Regex("^(univLoad|loadUniv|ul|lu)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex DataSearch =
            new Regex("^(dataSearch|searchData|ds|sd)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex ProblemCreate =
            new Regex("^(probCreate|createProb|prc|cpr)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex PoiCreate =
            new Regex("^(poiCreate|createPoi|poic|cpoi)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex ShipCreate =
            new Regex("^(shipCreate|createShip|shc|csh)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex AlienCreate =
            new Regex("^(alienCreate|createAlien|ac|ca)($| .*)", RegexOptions.IgnoreCase);
        
        public static readonly Regex PrintGrid =
            new Regex("^(pg|printgrid)($| .*)", RegexOptions.IgnoreCase);
    }
}