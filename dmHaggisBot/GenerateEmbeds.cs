using Discord;
using SWNUniverseGenerator;

namespace dmHaggisBot
{
    internal class GenerateEmbeds
    {
        public static Embed CharacterEmbed(Character character)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkGrey);
            eb.Title = character.ID;

            eb.AddField("Name: ", character.Name);
            eb.AddField("Gender: ", character.GetGender());
            eb.AddField("Age: ", character.Age);
            eb.AddField("Hair Color: ", character.HairCol);
            eb.AddField("Hair Style: ", character.HairStyle);
            eb.AddField("Eye Color: ", character.EyeCol);
            eb.AddField("Birth Planet ID: ", character.BirthPlanet);
            eb.AddField("Current Location ID: ", character.CurrentLocation);

            return eb.Build();
        }
        
        public static Embed StarEmbed(Universe universe, Star star)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkBlue);
            eb.Title = star.ID;

            eb.AddField("Name: ", star.Name);
            eb.AddField("Grid: ", star.X + ", " + star.Y);
            string planets = "";
            foreach (Planet p in universe.Planets.FindAll(a => a.StarID == star.ID))
                planets += (p.ID + " - " + p.Name + "\n");

            eb.AddField("Planets", planets);

            return eb.Build();
        }
        
        public static Embed PlanetEmbed(Universe universe, Planet planet)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkGreen);
            eb.Title = planet.ID;
            eb.AddField("Name: ", planet.Name);
            eb.AddField("Star: ", planet.StarID + " - " +
                                  universe.Stars.Find(a => a.ID == planet.StarID).Name);

            return eb.Build();
        }
    }
}