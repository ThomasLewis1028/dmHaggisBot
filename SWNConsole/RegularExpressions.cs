using System.Text.RegularExpressions;

namespace SWNConsole
{
    /// <summary>
    /// This class contains a list of my regular expressions to be used in DiscordBot.cs
    ///
    /// This was created to clean up the bot file
    /// </summary>
    public class RegularExpressions
    {
        public static readonly Regex LoadUniverse =
            new("^(load(univ(erse)?)?|univ(erse)?load)($)", RegexOptions.IgnoreCase);

        public static readonly Regex CreateUniverse =
            new("^(create(univ(erse)?)?|univ(erse)?create)($)", RegexOptions.IgnoreCase);
        
        public static readonly Regex LoadUniverseName =
            new("^(load(univ(erse)?)?|univ(erse)?load)($| \".*\")", RegexOptions.IgnoreCase);

        public static readonly Regex CreateUniverseName =
            new("^(create(univ(erse)?)?|univ(erse)?create)($| \".*\")", RegexOptions.IgnoreCase);
        
        public static readonly Regex GetHelp =
            new("^help$", RegexOptions.IgnoreCase);
        
        public static readonly Regex ListUniverses =
            new("^(list(univ(erses)?)?|univ(erses)?list)($)", RegexOptions.IgnoreCase);
        
        public static readonly Regex CharacterCreate =
            new Regex("^(charCreate|createChar|cc)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex StarCreate =
            new Regex("^(starCreate|createStar|sc|cs)($| .*)", RegexOptions.IgnoreCase);

        public static readonly Regex PlanetCreate =
            new Regex("^(planetCreate|createPlanet|pc|cp)($| .*)", RegexOptions.IgnoreCase);

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