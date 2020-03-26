# SWNUniverseGenerator and dmHaggisBot

This project is currently two different projects (likely will split later) that consist of a generator for a Stars Without Number tabletop game, and a Discord bot that acts as a front-end.

## SWNUniverseGenerator

The SWNUniverseGenerator is a project that works with serialized json files to generate randomized data for a Stars Without Number tabletop game. This includes personData.json, worldTags.json, starData.json and others to be added as I create the classes and gather information. Data such as character names and hairstyles or planet/star names were gathered from various sources including [Mongo Bay](https://names.mongabay.com/) or Wikipedia.

There is a single entry-point to the data and that is through `Creation.cs` and all other function are marked as internal and private.

Data such as worldTags was gathered from the Stars Without Number rulebook and is set up in a way that data can be randomized on a bell-curve with running two dice rolls.

### Example
```csharp
planet.StarID = universe.Stars[rand.Next(0, universe.Stars.Count)].ID;
planet.WorldTag = worldInfo.WorldTags[rand.Next(0, 100)];
planet.Atmosphere = worldInfo.Atmospheres[rand.Next(0, 6) + rand.Next(0, 6)];
planet.Temperature = worldInfo.Temperatures[rand.Next(0, 6) + rand.Next(0, 6)];
planet.Biosphere = worldInfo.Biospheres[rand.Next(0, 6) + rand.Next(0, 6)];
planet.Population = worldInfo.Populations[rand.Next(0, 6) + rand.Next(0, 6)];
planet.TechLevel = worldInfo.TechLevels[rand.Next(0, 6) + rand.Next(0, 6)];
planet.Origin = worldInfo.OWOrigins[rand.Next(0, 8)];
planet.Relationship = worldInfo.OWRelationships[rand.Next(0, 8)];
planet.Contact = worldInfo.OWContacts[rand.Next(0, 8)];
```

Where you see two random rolls, these are getting numbers on a bell curve that will allow you to land on items in the middle of the list more than items at the beginning or the end (more breathable atmospheres than dangerous ones).

### Order of Operations
For a universe to be properly set up, the first thing you generate is the universe with a grid and a name. The default values for this are `Grid(8, 10)` and a name of "universe". After this, it follows the order of Stars => Planets => Characters/Problems. As more are added they will likely be added after Planets or Characters so that no item is without a proper parent.

This is coulped with a flat database system where a Character will have a BirthPlanet, but a Planet does not have a list of Characters born there. Then using simple Linq queries you can find all Characters witha BirthPlanetID that matches a given Planet.ID.

## dmHaggisBot

This is a bot  uses the [Discord.net](https://github.com/discord-net/Discord.Net) API to connect to Discord and speak in a channel. Each task is called asynchronously and uses a set of regular expressions to determine what function to call.

This bot is not meant to be extended from, but rather if you want to copy it into your own bot and use the same system I use here then go ahead. It is very specific to my needs as I create commands related to the SWNUnviverseGenerator or more methods for my own servers.
