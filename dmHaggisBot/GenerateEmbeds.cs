using System;
using System.Linq;
using System.Text;
using Discord;
using Microsoft.VisualBasic.CompilerServices;
using SWNUniverseGenerator;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace dmHaggisBot
{
    internal class GenerateEmbeds
    {
        public static Embed CharacterEmbed(Universe universe, Character character, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkGrey);
            eb.Title = character.ID;

            eb.AddField("Name: ", character.Name);
            eb.AddField("Gender: ", character.GetGender());
            eb.AddField("Age: ", character.Age);
            if (!string.IsNullOrEmpty(character.Title))
                eb.AddField("Title:", character.Title);
            if (!string.IsNullOrEmpty(character.ShipID))
                eb.AddField("Ship: ", character.ShipID);
            eb.AddField("Hair Color: ", character.HairCol);
            eb.AddField("Hair Style: ", character.HairStyle);
            eb.AddField("Eye Color: ", character.EyeCol);
            eb.AddField("Birth Planet: ",
                character.BirthPlanet + " - " + universe.Planets.Single(a => a.ID == character.BirthPlanet).Name);
            
            if (dmChannel)
            {
                eb.AddField("Current Location: ",
                             character.CurrentLocation + " - " +
                             universe.Planets.Single(a => a.ID == character.CurrentLocation).Name);
                eb.AddField("Crime Chance: ", character.CrimeChance + "%");
            }

            return eb.Build();
        }

        public static Embed StarEmbed(Universe universe, Star star, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.LightOrange);
            eb.Title = star.ID;

            eb.AddField("Name: ", star.Name);
            eb.AddField("Zone: ", universe.Zones.Single(a => a.StarID == star.ID).GetHex);
            string planets = "";
            foreach (Planet p in universe.Planets.FindAll(a => a.StarID == star.ID))
                planets += (p.ID + " - " + p.Name + "\n");

            if (!string.IsNullOrEmpty(planets))
                eb.AddField("Planets", planets);

            return eb.Build();
        }

        public static Embed PlanetEmbed(Universe universe, Planet planet, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkGreen);
            eb.Title = planet.ID;
            eb.AddField("Name: ", planet.Name);
            eb.AddField("Star: ", planet.StarID + " - " +
                                  universe.Stars.Single(a => a.ID == planet.StarID).Name);
            eb.AddField("World Tags: ", planet.FirstWorldTag.Type + ", " + planet.SecondWorldTag.Type);
            eb.AddField("Atmosphere: ", planet.Atmosphere.Type);
            eb.AddField("Biosphere: ", planet.Biosphere.Type);
            eb.AddField("Temperature: ", planet.Temperature.Type);
            eb.AddField("Population: ", planet.Population.Type);
            eb.AddField("Tech Level: ", planet.TechLevel.Type);
            eb.AddField("Primary World: ", planet.IsPrimary ? "Yes" : "No");
            if (!planet.IsPrimary)
            {
                eb.AddField("Origin: ", planet.Origin);
                eb.AddField("Relationship: ", planet.Relationship);
                eb.AddField("Contact: ", planet.Contact);
            }

            return eb.Build();
        }
        
        public static Embed ShipEmbed(Universe universe, Ship ship, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.LighterGrey);
            eb.Title = ship.ID;
            // eb.AddField("Name: ", ship.Name);
            eb.AddField("Hull: ", ship.Hull.Type);
            eb.AddField("Class: ", ship.Hull.Class);
            eb.AddField("Total Cost: ", ship.TotalCost());

            return eb.Build();
        }

        public static Embed ProblemEmbed(Universe universe, Problem problem, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkRed);
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


        public static Embed POIEmbed(Universe universe, PointOfInterest poi, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.Gold);
            eb.Title = poi.ID;
            eb.AddField("Name: ", poi.Name);
            eb.AddField("Type: ", poi.Type);
            eb.AddField("Situation: ", poi.Situation);
            eb.AddField("Occupied By: ", poi.OccupiedBy);
            eb.AddField("Location: ", poi.StarID + " - " +
                                      Search.SearchUniverse(universe,
                                          new SearchDefaultSettings {ID = new[] {poi.StarID}}).Result.Name);

            return eb.Build();
        }
        
        public static Embed ZoneEmbed(Universe universe, Zone zone, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.Blue);
            eb.Title = zone.GetHex;
            eb.AddField("Star: ", zone.StarID != null ? zone.StarID : "No System");
            if (zone.Planets.Count != 0)
            {
                var sb = new StringBuilder();
                zone.Planets.ForEach(a => sb.Append(a + "\n"));
                eb.AddField("Planets: ", sb);
            }
            if (zone.PointsOfInterest.Count != 0)
            {
                var sb = new StringBuilder();
                zone.PointsOfInterest.ForEach(a => sb.Append(a + "\n"));
                eb.AddField("Points of Interest: ", sb);
            }

            return eb.Build();
        }
    }
}