using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Discord;
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
            eb.Title = character.Id;

            eb.AddField("Name: ", character.Name);
            eb.AddField("Gender: ", character.GetGender());
            eb.AddField("Age: ", character.Age);
            if (!string.IsNullOrEmpty(character.Title))
                eb.AddField("Title:", character.Title);
            if (!string.IsNullOrEmpty(character.ShipId))
                eb.AddField("Ship: ", character.ShipId);
            eb.AddField("Hair Color: ", character.HairCol);
            eb.AddField("Hair Style: ", character.HairStyle);
            eb.AddField("Eye Color: ", character.EyeCol);
            eb.AddField("Birth Planet: ",
                character.BirthPlanet + " - " + universe.Planets.Single(a => a.Id == character.BirthPlanet).Name);

            if (dmChannel)
            {
                eb.AddField("Current Location: ",
                    character.CurrentLocation + " - " +
                    universe.Planets.Single(a => a.Id == character.CurrentLocation).Name);
                eb.AddField("Crime Chance: ", character.CrimeChance + "%");
                eb.AddField("Initial Reaction: ", character.InitialReaction);
            }

            return eb.Build();
        }

        public static Embed StarEmbed(Universe universe, Star star, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.LightOrange);
            eb.Title = star.Id;

            eb.AddField("Name: ", star.Name);
            eb.AddField("Zone: ", universe.Zones.Single(a => a.StarId == star.Id).GetHex);
            string planets = "";
            foreach (Planet p in universe.Planets.FindAll(a => a.StarId == star.Id))
                planets += (p.Id + " - " + p.Name + "\n");

            if (!string.IsNullOrEmpty(planets))
                eb.AddField("Planets", planets);

            return eb.Build();
        }

        public static Embed PlanetEmbed(Universe universe, Planet planet, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkGreen);
            eb.Title = planet.Id;
            eb.AddField("Name: ", planet.Name);
            if (dmChannel)
                eb.AddField("Star: ", planet.StarId + " - " +
                                      universe.Stars.Single(a => a.Id == planet.StarId).Name);
            eb.AddField("World Tags: ", planet.FirstWorldTag + ", " + planet.SecondWorldTag);
            eb.AddField("Atmosphere: ", planet.Atmosphere);
            eb.AddField("Biosphere: ", planet.Biosphere);
            eb.AddField("Temperature: ", planet.Temperature);
            eb.AddField("Population: ", planet.Population);
            eb.AddField("Tech Level: ", planet.TechLevel);
            eb.AddField("Primary World: ", planet.IsPrimary ? "Yes" : "No");
            if (!planet.IsPrimary)
            {
                eb.AddField("Origin: ", planet.Origin);
                eb.AddField("Relationship: ", planet.Relationship);
                eb.AddField("Contact: ", planet.Contact);
            }

            if (dmChannel)
            {
                eb.AddField("Society",
                    $"Prior Culture - {planet.Society.PriorCulture}" +
                    $"\nOther Society - {planet.Society.OtherSociety}" +
                    $"\nMain Remnant - {planet.Society.MainRemnant}" +
                    $"\nSociety Age - {planet.Society.SocietyAge}" +
                    $"\nImportant Resource - {planet.Society.ImportantResource}" +
                    $"\nFounding Reason - {planet.Society.FoundingReason}");

                eb.AddField("Ruler",
                    $"General Security - {planet.Ruler.GeneralSecurity}" +
                    $"\nLegitimacy Source - {planet.Ruler.LegitimacySource}" +
                    $"\nMain Ruler Conflict - {planet.Ruler.MainRulerConflict}" +
                    $"\nRule Completion - {planet.Ruler.RuleCompletion}" +
                    $"\nRule Form - {planet.Ruler.RuleForm}" +
                    $"\nMain Population Conflict - {planet.Ruler.MainPopConflict}");

                eb.AddField("Ruled",
                    $"\nContentment - {planet.Ruled.Contentment}" +
                    $"\nLast Major Threat - {planet.Ruled.LastMajorThreat}" +
                    $"\nPower - {planet.Ruled.Power}" +
                    $"\nUniformity - {planet.Ruled.Uniformity}" +
                    $"\nMain Conflict - {planet.Ruled.MainConflict}" +
                    $"\nTrends - {planet.Ruled.Trends}");

                eb.AddField("Flavor",
                    $"\nBasic Trends - {planet.Flavor.BasicFlavor}" +
                    $"\nOutsider Treatment - {planet.Flavor.OutsiderTreatment}" +
                    $"\nPrimary Virtue - {planet.Flavor.PrimaryVirtue}" +
                    $"\nPrimary Vice - {planet.Flavor.PrimaryVice}" +
                    $"\nXenophilia Degree - {planet.Flavor.XenophiliaDegree}" +
                    $"\nPossible Patron - {planet.Flavor.PossiblePatron}" +
                    $"\nCustoms - {planet.Flavor.Customs}");
            }

            return eb.Build();
        }

        public static Embed ShipEmbed(Universe universe, Ship ship, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.LighterGrey);
            eb.Title = ship.Id;
            //eb.AddField("Name: ", ship.Name);
            eb.AddField("Hull: ", Creation.ShipData.Hulls.Find(a => a.Type == ship.Hull)?.Type);
            eb.AddField("Class: ", Creation.ShipData.Hulls.Find(a => a.Type == ship.Hull)?.Class);
            int totalCost = 0;
            totalCost += Creation.ShipData.Hulls.Find(a => a.Type == ship.Hull).Cost;


            if (ship.Weapons != null)
            {
                StringBuilder wsb = new StringBuilder();

                var wl = ship.Weapons.GroupBy(w => w)
                    .Select(w => new
                    {
                        Count = w.Count(),
                        Name = w.Key
                    });

                foreach (var w in wl)
                {
                    wsb.Append(w.Count + "x " + w.Name + "\n");
                    totalCost += w.Count * Creation.ShipData.Weapons.Find(a => a.Type == w.Name).Cost;
                }

                eb.AddField("Weapons: ", wsb.ToString());
            }

            if (ship.Defenses != null)
            {
                StringBuilder dsb = new StringBuilder();

                foreach (var d in ship.Defenses)
                {
                    dsb.Append(d + "\n");
                    totalCost += Creation.ShipData.Defenses.Find(a => a.Type == d).Cost;
                }

                eb.AddField("Defenses: ", dsb.ToString());
            }

            if (ship.Fittings != null)
            {
                StringBuilder fsb = new StringBuilder();

                var fl = ship.Fittings.GroupBy(f => f)
                    .Select(f => new
                    {
                        Count = f.Count(),
                        Name = f.Key
                    });

                foreach (var f in fl)
                {
                    fsb.Append(f.Count + "x " + f.Name + "\n");
                    totalCost += f.Count * (int) Creation.ShipData.Fittings.Find(a => a.Type == f.Name).Cost;
                }

                eb.AddField("Fittings: ", fsb.ToString());
            }


            eb.AddField("Total Cost: ", (totalCost).ToString("#,###"));

            return eb.Build();
        }

        public static Embed AlienEmbed(Universe universe, Alien alien, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.LightOrange);
            eb.Title = alien.Id;

            // eb.AddField("Name: ", alien.Name);
            StringBuilder btsb = new StringBuilder();
            foreach (var bt in alien.BodyTraits)
                btsb.Append(bt + "\n");
            eb.AddField("Body Traits: ", btsb.ToString());

            StringBuilder lsb = new StringBuilder();
            foreach (var l in alien.Lenses)
                lsb.Append(l + "\n");
            eb.AddField("Lenses: ", lsb.ToString());

            StringBuilder ssb = new StringBuilder();
            foreach (var l in alien.SocialStructures)
                ssb.Append(l + "\n");
            eb.AddField(
                "Social Structures" + (alien.MultiPolarType == null ? ": " : " (" + alien.MultiPolarType + "):"),
                ssb.ToString());

            return eb.Build();
        }

        public static Embed ProblemEmbed(Universe universe, Problem problem, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.DarkRed);
            eb.Title = problem.Id;
            eb.AddField("Type: ", problem.ConflictType);
            eb.AddField("Situation: ", problem.Situation);
            eb.AddField("Focus: ", problem.Focus);
            eb.AddField("Restraint: ", problem.Restraint);
            eb.AddField("Twist: ", problem.Twist);
            eb.AddField("Location: ", problem.LocationId + " - " +
                                      Search.SearchUniverse(universe,
                                          new SearchDefaultSettings {Id = new[] {problem.LocationId}}).Result.Name);

            return eb.Build();
        }


        public static Embed PoiEmbed(Universe universe, PointOfInterest poi, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.Gold);
            eb.Title = poi.Id;
            eb.AddField("Name: ", poi.Name);
            eb.AddField("Type: ", poi.Type);
            eb.AddField("Situation: ", poi.Situation);
            eb.AddField("Occupied By: ", poi.OccupiedBy);
            eb.AddField("Location: ", poi.StarId + " - " +
                                      Search.SearchUniverse(universe,
                                          new SearchDefaultSettings {Id = new[] {poi.StarId}}).Result.Name);

            return eb.Build();
        }

        public static Embed ZoneEmbed(Universe universe, Zone zone, Boolean dmChannel)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.WithColor(Color.Blue);
            eb.Title = zone.GetHex;
            eb.AddField("Star: ", zone.StarId != null ? zone.StarId : "No System");
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