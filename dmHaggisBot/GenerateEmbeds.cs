﻿using System.Linq;
using Discord;
using SWNUniverseGenerator;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace dmHaggisBot
{
    internal class GenerateEmbeds
    {
        public static Embed CharacterEmbed(Universe universe, Character character)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkGrey);
            eb.Title = character.ID;

            eb.AddField("Name: ", character.Name);
            eb.AddField("Gender: ", character.GetGender());
            eb.AddField("Age: ", character.Age);
            if (!string.IsNullOrEmpty(character.Title))
                eb.AddField("Title:", character.Title);
            eb.AddField("Hair Color: ", character.HairCol);
            eb.AddField("Hair Style: ", character.HairStyle);
            eb.AddField("Eye Color: ", character.EyeCol);
            eb.AddField("Birth Planet: ",
                character.BirthPlanet + " - " + universe.Planets.Single(a => a.ID == character.BirthPlanet).Name);
            eb.AddField("Current Location: ",
                character.CurrentLocation + " - " +
                universe.Planets.Single(a => a.ID == character.CurrentLocation).Name);

            return eb.Build();
        }

        public static Embed StarEmbed(Universe universe, Star star)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkBlue);
            eb.Title = star.ID;

            eb.AddField("Name: ", star.Name);
            eb.AddField("Zone: ", star.GetZone);
            string planets = "";
            foreach (Planet p in universe.Planets.FindAll(a => a.StarID == star.ID))
                planets += (p.ID + " - " + p.Name + "\n");

            if (!string.IsNullOrEmpty(planets))
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
                                  universe.Stars.Single(a => a.ID == planet.StarID).Name);

            return eb.Build();
        }

        public static Embed ProblemEmbed(Universe universe, Problem problem)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkGreen);
            eb.Title = problem.ID;
            eb.AddField("Type: ", problem.ConflictType);
            eb.AddField("Situation: ", problem.Situation);
            eb.AddField("Focus: ", problem.Focus);
            eb.AddField("Restraint: ", problem.Restraint);
            eb.AddField("Twist: ", problem.Twist);
            eb.AddField("Location: ", problem.LocationID + " - " +
                                      Search.SearchUniverse(universe,
                                          new SearchDefaultSettings {ID = new[] {problem.LocationID}}).Result.Name);

            return eb.Build();
        }
    }
}