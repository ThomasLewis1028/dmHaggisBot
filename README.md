# Stars Without Number Generator Tool

This project is a generator for a Stars Without Number tabletop game.

The idea came when I was thinking about DMing a game of Stars Without Number. The only time I have had the pleasure of playing SWN was when I was in high school, and the GM that we had was awesome. One of the things he did very well was that he could come up with characters on the spot. This was great, but every now and then, he would forget what characters he had come up with or what they were like.

This got me thinking, "What if I could generate a character on the fly with a basic description, a name, maybe where they were from, and what they do?" So I took to my development environment and started making a little character generator. This worked pretty well, however it had one glaring issue.

To quote Carl Sagan, "If you wish to make an apple pie from scratch, you must first invent the universe."

From the initial idea it spiraled. "Well if I can make a character, maybe I could make a planet name that they're from?" Then it was "Well if I can generate a planet I can generate a star?" Then it spiraled and now it's the behemoth you see today.

The goals for the project are as follows:
- [X] Generate a simple hex grid system of X by Y
- [X] Generate stars that live in the created universe system
- [X] Generate planets that are tied to a given star in the system
- [X] Generate a vector-based image that displays the system map
- [X] Generate characters that live in the system
- [X] Generate ships of various sizes and classes
- [X] Generate points of interest that can be littered throughout the system
- [X] Generate problems that the player can encounter
- [ ] Generate cities that exist on planets
- [X] Generate aliens that are unique and interesting
- [ ] Generate buildings and places that exist on places
- [ ] Generate faction, organization, and company names
- [ ] Assign NPCs to ships for realistic "who is the captain of that ship?" encounters
- [ ] Assign jobs to locations, such as bartender or police officer
- [ ] Assign NPCs to jobs based on their location
- [ ] Create a full, text-based game off of this (probably not)
- [ ] Implement a web-based UI that can be self-hosted and shared with players
- [ ] Utilize role-based views that allows hiding of content from players

As you can see, I have high ambitions. If you look at my commit history, you can also see how often I go without touching this. Maybe someday, it'll be good enough for me to run a campaign. Maybe not. For now, it's fun.

## SWNUniverseGenerator
The SWNUniverseGenerator is the primary tool for the creation of the universe.

The SWNUG relies on a set of JSON files to create the foundation for which the universe can be built upon. This includes personData.json, worldTags.json, starData.json and others to be added as I create the classes and gather information. Data such as character names and hairstyles or planet/star names were gathered from various sources including [Mongo Bay](https://names.mongabay.com/) or Wikipedia.

City name list was taken from [Simple Maps](https://simplemaps.com/data/world-cities) using their free city list.

The information is stored in a SQLite database file using a code-first methodology where I built the classes I needed, then build the database from those connections.

There is a single entry-point to the data and that is through `Creation.cs` and all other function are marked as internal and private.

Data such as worldTags was gathered from the Stars Without Number rulebook and is set up in a way that data can be randomized on a bell-curve with running two dice rolls.

### Order of Operations
For a universe to be properly set up, the first thing you generate is the universe with a grid and a name. The default values for this are `Grid(8, 10)` and a name of "universe". After this, it follows the order of Stars => Planets => Characters/Problems. As more are added they will likely be added after Planets or Characters so that no item is without a proper parent.

This is coupled with a flat database system where a Character will have a BirthPlanet, but a Planet does not have a list of Characters born there. Then using simple Linq queries you can find all Characters with a BirthPlanetID that matches a given Planet.ID.

## SWNTests
This is the testing app. It can _technically_ be used to generate a universe for you, however they have weird names and, by default, they delete the data when the test passes.

My goal is 100% coverage.

It may not happen this way.

## SWBlazorApp
This will _eventually_ be the frontend. The goal is to allow for all creation to be started from the UI, and for all data to be visualized there as well.

It's not currently working because I broke it for the database. It'll get there.

## DEPRECATED - dmHaggisBot 
THIS IS NO LONGER IN USE. USE AT YOUR OWN RISK. WHEN `Discord.net` UPDATED I DECIDED I DIDN'T CARE TO FIGURE OUT HOW IT WORKED SO I NO LONGER USE THIS.

This is a bot  uses the [Discord.net](https://github.com/discord-net/Discord.Net) API to connect to Discord and speak in a channel. Each task is called asynchronously and uses a set of regular expressions to determine what function to call.

This bot is not meant to be extended from, but rather if you want to copy it into your own bot and use the same system I use here then go ahead. It is very specific to my needs as I create commands related to the SWNUnviverseGenerator or more methods for my own servers.
