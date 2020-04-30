using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace dmHaggisBot
{
    /// <summary>
    /// This bot acts as a front-end to my SWNUniverseGenerator
    ///
    /// Otherwise it doesn't do much.
    /// </summary>
    internal class DiscordBot
    {
        private static Universe _universe;
        private static Creation _creation;

        // Properties file
        private static readonly JObject Prop =
            JObject.Parse(
                File.ReadAllText(@"properties.json"));

        // Get the token out of the properties folder
        private readonly string _token;
        private readonly long _generalChannel;
        private readonly long _dmChannel;

        private readonly Regex _charCreate = new Regex("^(charCreate|createChar|cc)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _starCreate =
            new Regex("^(starCreate|createStar|sc|cs)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _planCreate =
            new Regex("^(planetCreate|createPlanet|pc|cp)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _univCreate =
            new Regex("^(univCreate|createUniv|uc|cu)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _univLoad = new Regex("^(univLoad|loadUniv|ul|lu)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _dataSearch =
            new Regex("^(dataSearch|searchData|ds|sd)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _probCreate =
            new Regex("^(probCreate|createProb|prc|cpr)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _poiCreate =
            new Regex("^(poiCreate|createPoi|poic|cpoi)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _shipCreate =
            new Regex("^(shipCreate|createShip|shc|csh)($| .*)", RegexOptions.IgnoreCase);

        // Set up emoji for pagination
        private static readonly Emoji RightArrow = new Emoji("▶️");
        private static readonly Emoji LeftArrow = new Emoji("◀️");

        // Discord config files
        private DiscordSocketClient _client;

        public DiscordBot(bool test)
        {
            _token =
                test ? (string) Prop.GetValue("tokenTest") : (string) Prop.GetValue("token");

            _generalChannel =
                test ? (long) Prop.GetValue("Test General") : (long) Prop.GetValue("General");

            _dmChannel =
                test ? (long) Prop.GetValue("Test DM") : (long) Prop.GetValue("DM Channel");
        }
        // private IConfiguration _config;

        // Set the path to the Universe files
        private static string UniversePath
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase!);
                var path = Uri.UnescapeDataString(uri.Path);
                if (!Directory.Exists(Path.GetDirectoryName(path) + "/UniverseFiles/"))
                    Directory.CreateDirectory(Path.GetDirectoryName(path) + "/UniverseFiles/");
                return Path.GetDirectoryName(path) + "/UniverseFiles/";
            }
        }

        public async Task MainAsync()
        {
            var config = new DiscordSocketConfig {MessageCacheSize = 100};
            // _client.Log += message => Console.Out.WriteLine();
            _client = new DiscordSocketClient(config);
            _client.MessageReceived += MessageReceived;
            _client.ReactionAdded += ReactionAdded;

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            await _client.SetGameAsync("No Universe Loaded");

            await Console.Out.WriteLineAsync("DiscordBot Connected");

            await Task.Delay(-1);
        }

        /// <summary>
        /// This method handles messages received on the Discord bot
        /// </summary>
        /// <param name="sm"></param>
        private async Task MessageReceived(SocketMessage sm)
        {
            if (sm.Author.IsBot)
                return;

            if ((long) sm.Channel.Id != _generalChannel && (long) sm.Channel.Id != _dmChannel)
                return;


            // Switch based on the set of regular expressions provided
            switch (sm.Content)
            {
                case var content when _univCreate.IsMatch(content): // Universe creation
                    await CreateUniv(sm);
                    break;
                case var content when _univLoad.IsMatch(content): // Universe loading
                    await LoadUniv(sm);
                    break;
                case var content when _starCreate.IsMatch(content): // Star creation
                    if (_universe != null)
                        await CreateStar(sm);
                    else
                        await sm.Channel.SendMessageAsync("No universe file loaded");
                    break;
                case var content when _planCreate.IsMatch(content): // Planet creation
                    if (_universe != null)
                        await CreatePlanet(sm);
                    else
                        await sm.Channel.SendMessageAsync("No universe file loaded");
                    break;
                case var content when _charCreate.IsMatch(content): // Character creation
                    if (_universe != null)
                        await CreateChar(sm);
                    else
                        await sm.Channel.SendMessageAsync("No universe file loaded");
                    break;
                case var content when _probCreate.IsMatch(content): // Problem creation
                    if (_universe != null)
                        await CreateProb(sm);
                    else
                        await sm.Channel.SendMessageAsync("No universe file loaded");
                    break;
                case var content when _poiCreate.IsMatch(content): // Point of Interest creation
                    if (_universe != null)
                        await CreatePoi(sm);
                    else
                        await sm.Channel.SendMessageAsync("No universe file loaded");
                    break;
                case var content when _dataSearch.IsMatch(content): // Searching
                    if (_universe != null)
                        await SearchData(sm);
                    else
                        await sm.Channel.SendMessageAsync("No universe file loaded");
                    break;
                case var content when Regex.IsMatch("pg", content, RegexOptions.IgnoreCase):
                    if (_universe != null)
                        await PrintGrid(sm);
                    else
                        await sm.Channel.SendMessageAsync("No universe file loaded");
                    break;
                case var content when _shipCreate.IsMatch(content): // Ship creation
                    if (_universe != null)
                        await CreateShip(sm);
                    else
                        await sm.Channel.SendMessageAsync("No universe file loaded");
                    break;
            }
        }

        /// <summary>
        /// This method handles reactions added to a message
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="sc"></param>
        /// <param name="sr"></param>
        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> cache, ISocketMessageChannel sc,
            SocketReaction sr)
        {
            var user = _client.GetUser(sr.UserId);
            if (user.IsBot)
                return;

            if (!_dataSearch.IsMatch(sr.Message.ToString()))
                return;

            var up = true;

            if (sr.Emote.ToString()!.Equals(RightArrow.ToString()))
                // ReSharper disable once RedundantAssignment
                up = true;

            else if (sr.Emote.ToString()!.Equals(LeftArrow.ToString()))
                up = false;

            var (searchDefaultSettings, message) = ParsePagination(sr, up);

            await sr.Message.Value.RemoveReactionAsync(sr.Emote, user);
            await SearchData(sr.Channel, message, searchDefaultSettings, cache.Value);
        }

        /// <summary>
        /// This method handles creating a Universe from a SocketMessage
        /// </summary>
        /// <param name="sm"></param>
        private async Task CreateUniv(SocketMessage sm)
        {
            var g = ParseCommand("g", sm.Content);
            var n = ParseCommand("n", sm.Content);
            var o = ParseCommand("o", sm.Content);

            var univDef = new UniverseDefaultSettings
            {
                Name = n,
                Grid = string.IsNullOrEmpty(g)
                    ? null
                    : g.Split(" ").Length == 1
                        ? new Grid(Int32.Parse(g), Int32.Parse(g))
                        : new Grid(Int32.Parse(g.Split(" ")[0]), Int32.Parse(g.Split(" ")[1])),
                Overwrite = o
            };


            try
            {
                _creation = new Creation(UniversePath);
                _universe = _creation.CreateUniverse(univDef);

                await sm.Channel.SendMessageAsync("Created Universe with the name " + _universe.Name);
                await SetGameStatus();
            }
            catch (IOException e)
            {
                await sm.Channel.SendMessageAsync(e.ToString());
            }
        }

        /// <summary>
        /// This method handles loading a Universe from a SocketMessage
        /// </summary>
        /// <param name="sm"></param>
        private async Task LoadUniv(SocketMessage sm)
        {
            var n = ParseCommand("n", sm.Content);

            try
            {
                _creation = new Creation(UniversePath);
                _universe = _creation.LoadUniverse(n);
                await sm.Channel.SendMessageAsync("Loaded Universe with the name " + _universe.Name);
                await SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }

        /// <summary>
        /// This method handles creating stars in a Universe from a SocketMessage
        /// </summary>
        /// <param name="sm"></param>
        private async Task CreateStar(SocketMessage sm)
        {
            var c = ParseCommand("c", sm.Content);

            var starDef = new StarDefaultSettings
            {
                StarCount = string.IsNullOrEmpty(c)
                    ? -1
                    : Int32.Parse(c)
            };


            try
            {
                _universe = _creation.CreateStars(_universe, starDef);
                await sm.Channel.SendMessageAsync(c + " stars created in " + _universe.Name);
                await SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }

        /// <summary>
        /// This method handles creating planets in a Universe from a SocketMessage
        /// </summary>
        /// <param name="sm"></param>
        private async Task CreatePlanet(SocketMessage sm)
        {
            var r = ParseCommand("r", sm.Content);
            var pr = string.IsNullOrEmpty(r)
                ? new[] {0, 0}
                : r.Split(" ").Length == 1
                    ? new[] {Int32.Parse(r), Int32.Parse(r)}
                    : new[] {Int32.Parse(r.Split(" ")[0]), Int32.Parse(r.Split(" ")[1])};

            var planDef = new PlanetDefaultSettings {PlanetRange = pr};

            try
            {
                _universe = _creation.CreatePlanets(_universe, planDef);
                await sm.Channel.SendMessageAsync("Up to " + pr[1] + " planets created for each star" + _universe.Name);
                await SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }

        /// <summary>
        /// This method handles creating characters in a Universe from a SocketMessage
        /// </summary>
        /// <param name="sm"></param>
        private async Task CreateChar(SocketMessage sm)
        {
            var a = ParseCommand("a", sm.Content);
            var ar = string.IsNullOrEmpty(a)
                ? new[] {-1, -1}
                : a.Split(" ").Length == 1
                    ? new[] {Int32.Parse(a), Int32.Parse(a),}
                    : new[] {Int32.Parse(a.Split(" ")[0]), Int32.Parse(a.Split(" ")[1])};
            var c = ParseCommand("c", sm.Content);
            var g = ParseCommand("g", sm.Content);
            var f = ParseCommand("f", sm.Content);
            var l = ParseCommand("l", sm.Content);
            var h = ParseCommand("h", sm.Content);
            var hc = ParseCommand("hc", sm.Content);
            var ec = ParseCommand("ec", sm.Content);
            var t = ParseCommand("t", sm.Content);

            // await sm.Channel.SendMessageAsync(sb.ToString());
            var charDef = new CharacterDefaultSettings
            {
                Count = string.IsNullOrEmpty(c) ? 1 : int.Parse(c),
                First = f,
                Last = l,
                Age = ar,
                HairStyle = h,
                HairCol = hc,
                EyeCol = ec,
                Title = t
            };


            if (g == "0")
                charDef.Gender = Character.GenderEnum.Male;
            else if (g == "1")
                charDef.Gender = Character.GenderEnum.Female;
            else
                charDef.Gender = Character.GenderEnum.Undefined;

            try
            {
                _universe = _creation.CreateCharacter(_universe, charDef);

                await sm.Channel.SendMessageAsync(charDef.Count + " new character(s) created in " + _universe.Name);
                await SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }

        /// <summary>
        /// This method handles creating problems in a Universe from a SocketMessage
        /// </summary>
        /// <param name="sm"></param>
        private async Task CreateProb(SocketMessage sm)
        {
            var c = ParseCommand("c", sm.Content);
            var a = ParseCommand("a", sm.Content);
            var id = ParseCommand("id", sm.Content);

            var probDef = new ProblemDefaultSettings
            {
                Count = string.IsNullOrEmpty(c)
                    ? -1
                    : Int32.Parse(c),
                Additive = !string.IsNullOrEmpty(a) && a.ToUpper() == "Y",
                Id = string.IsNullOrEmpty(id)
                    ? null
                    : id
            };
            try
            {
                _universe = _creation.CreateProblems(_universe, probDef);
                await sm.Channel.SendMessageAsync(c + " problems created in " + _universe.Name);
                await SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }

        /// <summary>
        /// This method handles creating Points of Interest in a Universe from a SocketMessage
        /// </summary>
        /// <param name="sm"></param>
        private async Task CreatePoi(SocketMessage sm)
        {
            var c = ParseCommand("c", sm.Content);
            var cr = string.IsNullOrEmpty(c)
                ? new[] {-1, -1}
                : c.Split(" ").Length == 1
                    ? new[] {Int32.Parse(c), Int32.Parse(c),}
                    : new[] {Int32.Parse(c.Split(" ")[0]), Int32.Parse(c.Split(" ")[1])};
            var id = ParseCommand("id", sm.Content);
            var n = ParseCommand("n", sm.Content);

            var poiDef = new PoiDefaultSettings()
            {
                PoiRange = cr,
                StarId = id,
                Name = n
            };
            try
            {
                _universe = _creation.CreatePoi(_universe, poiDef);
                await sm.Channel.SendMessageAsync("Points of Interest created in " + _universe.Name);
                await SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }

        /// <summary>
        /// This method handles creating Ships in a Universe from a SocketMessage
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        private async Task CreateShip(SocketMessage sm)
        {
            var c = ParseCommand("c", sm.Content);
            var id = ParseCommand("id", sm.Content);
            var n = ParseCommand("n", sm.Content);
            var cid = ParseCommand("cid", sm.Content);
            var pid = ParseCommand("pid", sm.Content);
            var eid = ParseCommand("eid", sm.Content);
            var gid = ParseCommand("gid", sm.Content);
            var cmid = ParseCommand("cmid", sm.Content);
            var crid = ParseCommand("crid", sm.Content);
            var crl = string.IsNullOrEmpty(crid)
                ? null
                : crid.Split(" ").ToList();
            var t = ParseCommand("t", sm.Content);


            var shipDef = new ShipDefaultSettings
            {
                Count = string.IsNullOrEmpty(c)
                    ? -1
                    : Int32.Parse(c),
                Id = string.IsNullOrEmpty(id)
                    ? null
                    : id,
                Name = string.IsNullOrEmpty(n)
                    ? null
                    : n,
                CaptainId = string.IsNullOrEmpty(cid)
                    ? null
                    : cid,
                PilotId = string.IsNullOrEmpty(pid)
                    ? null
                    : pid,
                EngineerId = string.IsNullOrEmpty(eid)
                    ? null
                    : eid,
                CommsId = string.IsNullOrEmpty(cmid)
                    ? null
                    : cmid,
                GunnerId = string.IsNullOrEmpty(gid)
                    ? null
                    : gid,
                CrewId = crl,
                Type = string.IsNullOrEmpty(t)
                    ? null
                    : t
            };
            try
            {
                _universe = _creation.CreateShips(_universe, shipDef);
                await sm.Channel.SendMessageAsync("Ships created in " + _universe.Name);
                await SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }

        /// <summary>
        /// This method handles setting up an initial search through a Universe from a SocketMessage
        /// </summary>
        /// <param name="sm"></param>
        private async Task SearchData(SocketMessage sm)
        {
            var id = ParseCommand("id", sm.Content);
            var n = ParseCommand("n", sm.Content);
            var c = ParseCommand("c", sm.Content);
            var t = ParseCommand("t", sm.Content);
            var l = ParseCommand("l", sm.Content);

            SearchDefaultSettings searchDef = new SearchDefaultSettings
            {
                Id = string.IsNullOrEmpty(id)
                    ? new string[] { }
                    : id.Split(", "),
                Name = string.IsNullOrEmpty(n)
                    ? new string[] { }
                    : n.Split(", "),
                Index = string.IsNullOrEmpty(c)
                    ? 0
                    : Int32.Parse(c),
                Tag = string.IsNullOrEmpty(t)
                    ? new string[] { }
                    : t.Split(" "),
                Permission = _dmChannel == (long) sm.Channel.Id
                    ? SearchDefaultSettings.PermissionType.Dm
                    : SearchDefaultSettings.PermissionType.Player,
                Location = string.IsNullOrEmpty(l)
                    ? new string[] { }
                    : l.Split(", ")
            };


            await SearchData(sm.Channel, sm.Content, searchDef);
        }

        /// <summary>
        /// This method handles searching through a Universe from a SocketMessage or a SocketReaction
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="sm"></param>
        /// <param name="searchDefaultSettings"></param>
        /// <param name="userMessage"></param>
        private static async Task SearchData(ISocketMessageChannel sc, string sm,
            SearchDefaultSettings searchDefaultSettings, IUserMessage userMessage = null)
        {
            var results = Search.SearchUniverse(_universe, searchDefaultSettings);
            var embeds = new List<Embed>();

            if (results.Result != null)
            {
                switch (results.Result)
                {
                    case Character character:
                        embeds.Add(GenerateEmbeds.CharacterEmbed(_universe, character,
                            searchDefaultSettings.Permission == SearchDefaultSettings.PermissionType.Dm));
                        break;
                    case Planet planet:
                        embeds.Add(GenerateEmbeds.PlanetEmbed(_universe, planet,
                            searchDefaultSettings.Permission == SearchDefaultSettings.PermissionType.Dm));
                        break;
                    case Star star:
                        embeds.Add(GenerateEmbeds.StarEmbed(_universe, star,
                            searchDefaultSettings.Permission == SearchDefaultSettings.PermissionType.Dm));
                        break;
                    case Ship ship:
                        embeds.Add(GenerateEmbeds.ShipEmbed(_universe, ship,
                            searchDefaultSettings.Permission == SearchDefaultSettings.PermissionType.Dm));
                        break;
                    case Problem problem:
                        embeds.Add(GenerateEmbeds.ProblemEmbed(_universe, problem,
                            searchDefaultSettings.Permission == SearchDefaultSettings.PermissionType.Dm));
                        break;
                    case PointOfInterest pointOfInterest:
                        embeds.Add(GenerateEmbeds.PoiEmbed(_universe, pointOfInterest,
                            searchDefaultSettings.Permission == SearchDefaultSettings.PermissionType.Dm));
                        break;
                    case Zone zone:
                        embeds.Add(GenerateEmbeds.ZoneEmbed(_universe, zone,
                            searchDefaultSettings.Permission == SearchDefaultSettings.PermissionType.Dm));
                        break;
                }

                var message = sm + " - [" + results.CurrentIndex + ", " + results.MaxCount + "]";

                if (userMessage == null)
                {
                    RestUserMessage rsu = await sc.SendMessageAsync(message, false, embeds[0]);
                    await rsu.AddReactionAsync(LeftArrow);
                    await rsu.AddReactionAsync(RightArrow);
                }
                else
                {
                    await userMessage.ModifyAsync(x => x.Content = message);
                    await userMessage.ModifyAsync(x => x.Embed = embeds[0]);
                }
            }
            else
                await sc.SendMessageAsync("No results found");
        }

        /// <summary>
        /// This method handles parsing commands that are passed in from the user
        /// </summary>
        /// <param name="argName"></param>
        /// <param name="argVal"></param>
        /// <returns></returns>
        private static string ParseCommand(string argName, string argVal)
        {
            var start = argVal.IndexOf(" -" + argName + " ", StringComparison.Ordinal) + 1;
            if (start == 0)
                return null;

            var end = argVal.IndexOf(" -", start, StringComparison.Ordinal);
            if (end == -1)
                end = argVal.Length - 1;

            var cmd = argVal.Substring(start + 2 + argName.Length, end - start - argName.Length - 1);

            return cmd.Trim();
        }

        /// <summary>
        /// This method handles pagination through from a SocketReaction
        ///
        /// This receives a SocketReaction that includes the necessary commands and search query
        /// It also receives a boolean that determines whether the search is incrementing or decrementing
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="up"></param>
        /// <returns>
        /// Return a set of SearchDefaultSettings and the message stripped from the SocketReaction 
        /// </returns>
        private (SearchDefaultSettings, string) ParsePagination(SocketReaction sr, bool up)
        {
            var message = sr.Message.ToString();

            var id = ParseCommand("id", message);
            var n = ParseCommand("n", message);
            var c = ParseCommand("c", message);
            var t = ParseCommand("t", message);
            var l = ParseCommand("l", message);

            Match match = Regex.Match(message, @"\s\-\s\[\d+\,\s\d+\]$");
            if (match.Success)
            {
                var start = match.Index;
                var cs = message.Substring(start);
                var cn = message.Substring(start).Replace(" - [", "").Replace("]", "").Split(", ")[0].Trim();
                c = (Int32.Parse(cn) + (up ? 1 : -1)).ToString();
                message = message.Replace(cs, "");
            }

            SearchDefaultSettings searchDef = new SearchDefaultSettings
            {
                Id = string.IsNullOrEmpty(id)
                    ? new string[] { }
                    : id.Split(", "),
                Name = string.IsNullOrEmpty(n)
                    ? new string[] { }
                    : n.Split(", "),
                Index = Int32.Parse(c),
                Tag = string.IsNullOrEmpty(t)
                    ? new string[] { }
                    : t.Split(" "),
                Permission = _dmChannel == (long) sr.Channel.Id
                    ? SearchDefaultSettings.PermissionType.Dm
                    : SearchDefaultSettings.PermissionType.Player,
                Location = string.IsNullOrEmpty(l)
                    ? new string[] { }
                    : l.Split(", ")
            };

            return (searchDef, message);
        }

        /// <summary>
        /// This message receives a socket message to respond with rough square grid of the universe
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        private static async Task PrintGrid(SocketMessage sm)
        {
            var sb = new StringBuilder();
            sb.Append(_universe.Name);
            sb.Append("```\n");

            sb.Append("## ");
            for (var i = 0; i < _universe.Grid.X; i++)
                sb.Append(i < 10 ? "0" + i + " " : i + " ");

            sb.Append("\n");

            foreach (var z in _universe.Zones)
            {
                if (z.X == 0)
                    sb.Append(z.Y < 10 ? "0" + z.Y + " " : z.Y + " ");
                sb.Append(string.IsNullOrEmpty(z.StarId) ? ".  " : "X  ");
                if (z.X == _universe.Grid.X - 1)
                    sb.Append("\n");
            }

            sb.Append("```");

            await sm.Channel.SendMessageAsync(sb.ToString());
        }

        /// <summary>
        /// Set the game status of the bot to show the universe stats
        /// </summary>
        private async Task SetGameStatus()
        {
            await _client.SetGameAsync(_universe.Name + " Loaded - " +
                                       _universe.Stars.Count + " Stars - " +
                                       _universe.Planets.Count + " Planets - " +
                                       _universe.Ships.Count + " Ships - " +
                                       _universe.Characters.Count + " Characters - " +
                                       _universe.PointsOfInterest.Count + " Points of Interest - " +
                                       _universe.Problems.Count + " Problems");
        }
    }
}