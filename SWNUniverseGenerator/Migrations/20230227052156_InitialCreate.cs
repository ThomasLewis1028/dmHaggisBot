using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SWNUniverseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Problems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    LocationId = table.Column<string>(type: "TEXT", nullable: true),
                    ConflictType = table.Column<string>(type: "TEXT", nullable: true),
                    Situation = table.Column<string>(type: "TEXT", nullable: true),
                    Focus = table.Column<string>(type: "TEXT", nullable: true),
                    Restraint = table.Column<string>(type: "TEXT", nullable: true),
                    Twist = table.Column<string>(type: "TEXT", nullable: true),
                    UniverseId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipHull",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Cost = table.Column<int>(type: "INTEGER", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: true),
                    Armor = table.Column<int>(type: "INTEGER", nullable: false),
                    Hp = table.Column<int>(type: "INTEGER", nullable: false),
                    CrewMin = table.Column<int>(type: "INTEGER", nullable: false),
                    CrewMax = table.Column<int>(type: "INTEGER", nullable: false),
                    Ac = table.Column<int>(type: "INTEGER", nullable: false),
                    Power = table.Column<int>(type: "INTEGER", nullable: false),
                    Mass = table.Column<int>(type: "INTEGER", nullable: false),
                    Hardpoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Class = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipHull", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    GridX = table.Column<int>(type: "INTEGER", nullable: false),
                    GridY = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HullId = table.Column<string>(type: "TEXT", nullable: true),
                    CrewSkill = table.Column<int>(type: "INTEGER", nullable: false),
                    Cp = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeId = table.Column<string>(type: "TEXT", nullable: true),
                    LocationId = table.Column<string>(type: "TEXT", nullable: true),
                    UniverseId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ships_ShipHull_HullId",
                        column: x => x.HullId,
                        principalTable: "ShipHull",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ships_Universes_UniverseId",
                        column: x => x.UniverseId,
                        principalTable: "Universes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false),
                    UniverseId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Universes_UniverseId",
                        column: x => x.UniverseId,
                        principalTable: "Universes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipDefense",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Cost = table.Column<int>(type: "INTEGER", nullable: false),
                    CostExtra = table.Column<bool>(type: "INTEGER", nullable: false),
                    Power = table.Column<int>(type: "INTEGER", nullable: false),
                    Mass = table.Column<int>(type: "INTEGER", nullable: false),
                    MassExtra = table.Column<bool>(type: "INTEGER", nullable: false),
                    Class = table.Column<string>(type: "TEXT", nullable: true),
                    Effect = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ShipId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipDefense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipDefense_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShipFitting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Cost = table.Column<int>(type: "INTEGER", nullable: true),
                    CostExtra = table.Column<bool>(type: "INTEGER", nullable: false),
                    Power = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerExtra = table.Column<bool>(type: "INTEGER", nullable: false),
                    Mass = table.Column<double>(type: "REAL", nullable: false),
                    MassExtra = table.Column<bool>(type: "INTEGER", nullable: false),
                    Class = table.Column<string>(type: "TEXT", nullable: true),
                    Effect = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ShipId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipFitting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipFitting_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShipWeapon",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Cost = table.Column<int>(type: "INTEGER", nullable: false),
                    AmmoCost = table.Column<int>(type: "INTEGER", nullable: true),
                    Dmg = table.Column<string>(type: "TEXT", nullable: true),
                    Power = table.Column<int>(type: "INTEGER", nullable: false),
                    Mass = table.Column<int>(type: "INTEGER", nullable: false),
                    Hardpoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Class = table.Column<string>(type: "TEXT", nullable: true),
                    TechLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Qualities = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ShipId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipWeapon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipWeapon_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    UniverseId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FirstWorldTag = table.Column<string>(type: "TEXT", nullable: true),
                    SecondWorldTag = table.Column<string>(type: "TEXT", nullable: true),
                    Atmosphere = table.Column<string>(type: "TEXT", nullable: true),
                    Temperature = table.Column<string>(type: "TEXT", nullable: true),
                    Biosphere = table.Column<string>(type: "TEXT", nullable: true),
                    Population = table.Column<string>(type: "TEXT", nullable: true),
                    TechLevel = table.Column<string>(type: "TEXT", nullable: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false),
                    Origin = table.Column<string>(type: "TEXT", nullable: true),
                    Relationship = table.Column<string>(type: "TEXT", nullable: true),
                    Contact = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planets_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointsOfInterest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    OccupiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Situation = table.Column<string>(type: "TEXT", nullable: true),
                    UniverseId = table.Column<string>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsOfInterest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointsOfInterest_Universes_UniverseId",
                        column: x => x.UniverseId,
                        principalTable: "Universes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointsOfInterest_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stars",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UniverseId = table.Column<string>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    StarColor = table.Column<int>(type: "INTEGER", nullable: false),
                    StarClass = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stars_Universes_UniverseId",
                        column: x => x.UniverseId,
                        principalTable: "Universes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stars_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    First = table.Column<string>(type: "TEXT", nullable: true),
                    Last = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    BirthPlanetId = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentLocationId = table.Column<string>(type: "TEXT", nullable: true),
                    HairStyle = table.Column<string>(type: "TEXT", nullable: true),
                    HairCol = table.Column<string>(type: "TEXT", nullable: true),
                    EyeCol = table.Column<string>(type: "TEXT", nullable: true),
                    SkinCol = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    CrimeChance = table.Column<int>(type: "INTEGER", nullable: false),
                    UniverseId = table.Column<string>(type: "TEXT", nullable: false),
                    InitialReaction = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Planets_BirthPlanetId",
                        column: x => x.BirthPlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Universes_UniverseId",
                        column: x => x.UniverseId,
                        principalTable: "Universes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrewMember",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ShipId = table.Column<string>(type: "TEXT", nullable: true),
                    CharacterId = table.Column<string>(type: "TEXT", nullable: true),
                    CrewType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrewMember_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrewMember_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ShipDefense",
                columns: new[] { "Id", "Class", "Cost", "CostExtra", "Description", "Effect", "Mass", "MassExtra", "Power", "ShipId" },
                values: new object[,]
                {
                    { "Z-1561A7C3", "Frigate", 25000, true, "The ship has hardened bulkheads, reinforced hatches, and specially-designed automated kill corridors for wiping out intruders. Provided that the ship’s bridge is still under control, the operator can prevent entry to the ship by any force that lacks shipyard-grade tools, weapons capable of melting hull plating, or specialized military breaching implements. If intruders do get inside, only well-equipped, specially-trained marines have any real chance of breaching the defenses. Ordinary space pirates or more casual invaders have only a 1 in 6 chance of threatening the bridge crew, though they may cause significant damage in their dying.", "Makes enemy boarding more difficult", 1, true, 2, null },
                    { "Z-4ECC00C9", "Frigate", 10000, true, "Too small to damage ships, these point defense lasers can detonate or melt incoming munitions, improving the ship’s defenses against torpedoes, fractal impact charges, and other ammunition-based weapons.", "+2 AC versus weapons that use ammo", 2, true, 3, null },
                    { "Z-5F96CA37", "Fighter", 25000, true, "At the cost of a certain amount of speed and maneuverability, a ship can have its armor plating reinforced against glancing hits, gaining a +2 bonus to its AC. This augmentation can decrease a ship’s Speed below 0, meaning it will be applied as a penalty to all Pilot tests.", "+2 AC, -1 speed", 1, true, 0, null },
                    { "Z-8BD8EF81", "Frigate", 25000, true, "A high-powered secondary ECM generator can be activated to negate any one otherwise-successful hit against the ship. This generator can be activated after the damage has been rolled, but enemy ships rapidly compensate for the new ECM source, and so the generator can only be used effectively once per engagement", "Negate one successful hit", 1, true, 2, null },
                    { "Z-A55C0BDB", "Fighter", 25000, true, "A complex glazing process can harden the surface of a ship’s armor to more effectively shed incoming attacks, decreasing the armor-piercing quality of any hit by 5.", "AP quality of attacking weapons reduced by 5", 1, true, 0, null },
                    { "Z-B8F76B89", "Capital", 100000, true, "By sacrificing empty hull space in a complex system of ablative blast baffles, a capital-class ship can have a large amount of its total mass shot away without actually impinging on its normal function. This grants it a +1 AC bonus and 20 extra maximum hit points.", "+1 AC, +20 maximum hit points", 2, true, 5, null },
                    { "Z-E53618EC", "Frigate", 50000, true, "This system links with a ship’s navigational subsystem and randomizes the motion vectors in sympathy with metadimensional gravitic currents. This agility gives any hit on the ship a 1 in 6 chance of being negated entirely.", "1 in 6 change of any given attack missing", 2, true, 5, null },
                    { "Z-F5552F14", "Cruiser", 10000, true, "These drones are invariably short-lived due to the enormous energy signatures they produce, but until the ship’s next turn they grant a +2 AC bonus as their emissions confuse foes. Foxer drones are cheaply constructed and essentially free; the only limit on their number is the amount of free space set aside for holding them.", "+2 AC for one round when fired, ammo 5", 1, true, 2, null },
                    { "Z-F8DC5FBD", "Frigate", 50000, true, "The ship is equipped with an array of gravitic braker guns and an upgraded nuke snuffer field. While useless in conventional ship-to-ship combat, the array can deflect or dampen meteor impacts, dropped penetrator rods, or other non-powered bombardment techniques, and the snuffer field is powerful enough to prevent nuclear fission reactions over a hemisphere-sized area. A single ship with a PDA can protect against any natural meteor strikes and deny easy terror bombardment of a planet’s population. The PDA cannot fully protect against orbital strikes by powered penetrators, but it can nudge them off course and make pinpoint strikes impractical. Most developed planets have much more powerful and effective ground installations, but a PDA-equipped ship is a useful emergency stopgap for poor or primitive worlds.", "Anti-impact and anti-nuke surface defenses", 2, true, 4, null }
                });

            migrationBuilder.InsertData(
                table: "ShipFitting",
                columns: new[] { "Id", "Class", "Cost", "CostExtra", "Description", "Effect", "Mass", "MassExtra", "Power", "PowerExtra", "ShipId", "Type" },
                values: new object[,]
                {
                    { "Z-0052C9F1", "Frigate", null, false, "This TL5 tech is completely unavailable in most sectors and was uncommon even on Mandate-era ships. The pads allow up to a dozen people or 1,200 kilograms of matter to be teleported to and from the surface of a planet or the interior of a another ship, provided it is no more than a few tens of thousands of kilometers distant. Ship-to-ship teleportation is possible only when the receiving ship is cooperating by transmitting accurate coordinate details; otherwise, a friendly, unjammed signal from inside is necessary to lock onto the target point. Planetary teleportations are possible only in the absence of physical barriers between the ship and the target point below. The teleporters may be used once every five minutes. In those rare sectors where this tech is widely available, it costs 200k.", "Pretech teleportation to and from ship", 1.0, false, 1, false, null, "Teleportation pads" },
                    { "Z-0F431962", "Cruiser", 50000, true, "The ship is equipped with a full-scale TL4 fabrication plant programmed to support its needs. The ship can stock raw materials and parts at 5,000 credits per ton; these parts can then be used to “pay” ship repair or maintenance costs when conventional shipyards are unavailable. The factory can also create and repair vehicles, TL4 equipment, space habs, and planetary structures with these parts at a rate of 10,000 credits worth of construction a day. If a mobile extractor is available, the raw materials processed by the latter unit can be used to feed the factory. Operating a mobile factory requires at least 100 well-trained personnel. Each 10 fewer available doubles repair or maintenance times.", "Self-sustaining factory and repair facilities", 2.0, true, 3, false, null, "Mobile factory" },
                    { "Z-13FE957B", "Frigate", 10000, true, "The ship can disguise its long-range sensor readings, spoofing scans with the ID tags and apparent hull type of any other ship of its choice. To penetrate this masquerade, the scanning entity must beat a Wis/Program skill check against difficulty 10 plus the Program skill of the masking ship’s comms officer. Once the ship is close enough to visually identify, the masking is useless.", "At long distances, disguise ship as another", 0.0, false, 1, true, null, "Sensor mask" },
                    { "Z-15BC069C", "Frigate", 5000, true, "Most ships require only basic analysis of a star system, sufficient to identify population centers, do rough scanning of an object’s composition, and chart major navigational hazards. Survey sensor arrays greatly enhance the ship’s sensor abilities, allowing for finely-detailed mapping of objects and planets, along with broad-spectrum communications analysis. They also improve attempts to detect other craft when scanning a region for stealthy vessels. Any rolls with survey sensor arrays add +2 to skill checks.", "Improved planetary sensory array", 1.0, false, 2, false, null, "Survey sensor array" },
                    { "Z-2037FDA0", "Frigate", 10000, true, "A lab suitable for investigating alien xenolife, planetary geology, esoteric technology, and other mysteries of the cosmos. The lab contains cold sleep pods adequate to contain alien samples, viro-shielded research cells, high-energy lasers, and other requisite tools. When used to investigate some phenomenon or object, any applicable skill rolls are improved by +1 for a frigate lab, +2 for a cruiser lab, and +3 for a capital ship lab.", "Skill bonus for analysis and research", 2.0, false, 1, true, null, "Advanced lab" },
                    { "Z-23383C3B", "Fighter", 0, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "The standard spike drive common among all ships", 0.0, true, 0, true, null, "Drive-1" },
                    { "Z-2C5DE4D9", "Frigate", 5000, true, "All ships are equipped with basic medical facilities for curing lightly injured crew members and keeping the seriously injured ones stable until reaching a planet. An extended medbay improves those facilities, allowing for the medical treatment of up to the ship’s entire maximum crew at once, including the treatment of critically wounded passengers.", "Can provide medical care to more patients", 1.0, false, 1, false, null, "Extended medbay" },
                    { "Z-2D951008", "Fighter", null, false, "The ship’s standard drive-1 spike drive is removed and replaced with a different propulsion system. The ship is still treated as having a drive-1 for maneuvering and system transit purposes, but it cannot make interstellar drills. This modification lowers the cost of the basic hull by 10% and adds extra power and space based on the size of the hull: 1 power for fighters, 2 for frigates, 3 for cruisers, and 4 for capital ship hulls. Twice this amount of free mass is gained by the process.", "Replace spike drive with small system drive", -2.0, true, -1, true, null, "System drive" },
                    { "Z-30545E75", "Frigate", 50000, false, "Automated mining and refinery equipment has been built into the ship, allowing it to extract resources from asteroids and planetary surfaces. Careful extraction of specific lodes of valuable minerals can be quite profitable at the GM’s discretion, but if the crew is simply melting down available asteroids for raw materials, the unit can refine one ton of usable materials per day worth about 500 credits in most markets. These raw materials can be used to feed a mobile factory, and a ship may have more than one mobile extractor fitted to it to multiply the return if sufficient raw feedstock is available. Operating an extractor requires at least five crew members.", "Space mining and refinery fittings", 1.0, false, 2, false, null, "Mobile extractor" },
                    { "Z-3288F91A", "Frigate", 2500, true, "Selecting this fitting equips the ship with a number of single-use escape craft capable of reaching the nearest habitable planet or station in a star system. If no such destination exists, the boats can maintain their passengers for up to a year in drugged semi-stasis. Lifeboats have fully-functional comm systems and are usually equipped with basic survival supplies and distress beacons. A single selection of this fitting provides enough lifeboats for a ship’s maximum crew, with up to twenty people per boat.", "Emergency escape craft for a ship’s crew", 1.0, false, 0, false, null, "Lifeboats" },
                    { "Z-33F31DE2", "Fighter", 2500, true, "Most ships require refueling after each drill jump, no matter the distance. Installing fuel bunkers allows the ship to carry one additional load of fuel. This fitting can be installed multiple times for ships that wish to minimize fueling.", "Adds fuel for one more drill between fuelings", 1.0, false, 0, false, null, "Fuel bunkers" },
                    { "Z-33F74D70", "Cruiser", 200000, false, "These sophisticated docking bays provide all the necessary tools and support for launching a starship from the mother craft. They are rarely seen outside of dedicated capital-class carriers, but some cruisers make room to mount a fighter-class attack shuttle. Each bay allows room for one ship of the appropriate hull class. While the carried ship can support its own crew if necessary, most carriers fold their space wing into the mothership’s crew roster. This fitting can be taken multiple times.", "Carrier housing for a fighter", 2.0, false, 0, false, null, "Ship bay/fighter" },
                    { "Z-3417ED9D", "Frigate", 5000, true, "These stasis pods can keep a subject alive for centuries provided that the ship’s power doesn’t fail. Each installation allows for keeping a number of people equal to the ship’s maximum crew in stasis indefinitely. This fitting can be installed multiple times.", "Keeps occupants in stasis", 1.0, false, 1, false, null, "Cold sleep pods" },
                    { "Z-37C1BD38", "Frigate", 500, true, "On-board workshops can be bought at smaller than maximum sizes than a hull would allow, if additional TL4 tech facilities aren’t strictly needed. A frigate-sized workshop is sufficient for modding any personal gear as per the modification rules on page 100. A cruiser-sized workshop can handle vehicle modding and starship maintenance, and a capital-class workshop can build vehicles or similar large work from scratch at full cost.", "Automated tech workshops for maintenance", 0.5, true, 1, false, null, "Workshop" },
                    { "Z-39C9CA37", "Fighter", 10000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-2 rating", 1.0, true, 1, true, null, "Drive-2 upgrade" },
                    { "Z-3B0CC6AC", "Frigate", 25000, true, "Drills along a known spike drive route have no chance of failure so long as the route is no longer than twice the operating pilot’s skill level and does not involve course trimming. Thus, a navigator with Pilot-1 skill would always succeed in making a drill of two hexes or less, provided the route was a known one. The drill course regulator requires a sophisticated technical base and compatible metadimensional energy conditions in a given sector; many sectors no longer have this technology or lack the right environment to use it. If the GM prefers a campaign where space travel is always at least somewhat dangerous, they may deny access to this fitting. Ships piloted by exceptionally talented navigators may choose not to mount it even then.", "Common drill routes become auto-successes", 1.0, false, 1, true, null, "Drill course regulator" },
                    { "Z-3E30AF24", "Fighter", 5000, true, "This fitting must be put in place when the ship is built, and cannot be installed on cruiser-class or larger ships. A ship designed for atmospheric flight can land on most solid or aqueous surfaces.", "Can land: frigates and fighters only.", 1.0, true, 0, false, null, "Atmospheric configuration" },
                    { "Z-4742E3FD", "Capital", 1000000, false, "These sophisticated docking bays provide all the necessary tools and support for launching a starship from the mother craft. They are rarely seen outside of dedicated capital-class carriers, but some cruisers make room to mount a fighter-class attack shuttle. Each bay allows room for one ship of the appropriate hull class. While the carried ship can support its own crew if necessary, most carriers fold their space wing into the mothership’s crew roster. This fitting can be taken multiple times.", "Carrier housing for a frigate", 4.0, false, 1, false, null, "Ship bay/frigate" },
                    { "Z-4B90F8D6", "Frigate", 300000, false, "Armored and stealthed versions of cargo lighters, these craft are twice as fast, apply a -3 penalty to tracking and targeting skill checks, and can carry up to one hundred troops or passengers. Many are equipped with assorted Heavy weapons to clear the landing zone, and can be treated as flight-capable gravtanks for purposes of combat. This fitting can be purchased multiple times.", "Stealthed landing pod for troops", 2.0, false, 0, false, null, "Drop pod" },
                    { "Z-5024DCE7", "Fighter", 10000, true, "The ship has been carefully fitted to support the use of non-sentient expert system robots in its operation. At least one human, VI, or True AI crew member is necessary to oversee the bots and monitor spike drills, but otherwise crew may be replaced with cheap, basic robots at a cost of 1,000 credits per crew member replaced. Bots don’t draw pay, don’t take up life support, and their maintenance is assumed to be part of the ship’s operating costs. These bots are incapable of any actions unrelated to operating the ship and are treated as level-0 in their skills where relevant.", "Ship can use simple robots as crew", 1.0, false, 2, false, null, "Automation support" },
                    { "Z-594778BC", "Cruiser", 50000, true, "These banked rows of compressed cold sleep pods are designed to carry enormous numbers of people in extended hibernation, most often colonists to some new homeworld or escapees from some stellar disaster. A cruiser can carry up to 1,000 colonists in stasis, while a capital ship can handle up to 5,000. Each further time this fitting is selected, these numbers double. These pods put their inhabitants into very deep hibernation so as to minimize the resources necessary to maintain their lives. Bringing them out of this stasis requires a month of “defrost”. Crash awakenings have a 25% chance of killing the subject. The pods are rated for 100 years of stasis, but their actual maximum duration is somewhat speculative.", "House vast numbers of cold sleep passengers", 2.0, true, 1, true, null, "Exodus bay" },
                    { "Z-620B841C", "Fighter", 50000, false, "Some ships run with more guns than crewmen. A single NPC or PC can man one gun per round, firing it as often as the gunnery chief’s Fire One Weapon or Fire All Guns actions allow, but sometimes that’s not enough. Installing an autonomic targeting system for a gun allows it to shoot at a +2 hit bonus without human assistance. This system must be installed once for each gun that is to be self-manned.", "Fires one weapon system without a gunner", 0.0, false, 1, false, null, "Auto-targeting system" },
                    { "Z-6552E212", "Fighter", 2500, true, "A normal complement of ship’s stores can keep the maximum crew size supplied for two months. Each selection of Extended Stores doubles that time, and can be fitted multiple times.", "Maximum life support duration is doubled", 1.0, true, 0, false, null, "Extended stores" },
                    { "Z-71150C83", "Frigate", 2000, true, "Much like an armory, this option allows a captain to lay in a general supply of equipment likely to be useful to explorers and spacemen. Any TL4 equipment on the gear list can be found in the ship’s locker in amounts commensurate with the ship’s size. A few guns and some basic armor might be included as well, but for serious armament an armory is required. There is enough gear available to outfit the entire crew for normal use, but giving it away or losing it in use may deplete the locker until it is restocked.", "General equipment for the crew", 0.0, false, 0, false, null, "Ship’s locker" },
                    { "Z-76DD8176", "Fighter", 25000, true, "Stealth systems can mask the ship’s energy emissions through careful modulation of the output. All travel times inside a star system are doubled when the system is engaged, but any skill checks to avoid detection gain a +2 bonus.", "Adds +2 to skill checks to avoid detection", 1.0, true, 1, true, null, "Emissions dampers" },
                    { "Z-7AFD1613", "Fighter", 2500, true, " Carefully-designed storage space intended to conceal illicit cargo from customs inspection. Each installation of this fitting adds 200 kilograms of cargo space in a fighter, 2 tons in a frigate, 20 tons in a cruiser, or 200 tons in a capital ship. Cargo in a smuggler’s hold will never be found by a standard customs inspection. Careful investigation by a suspicious official can find it on a difficulty 10 check using their Wis/Notice skill, and a week-long search one step short of disassembly will find it on a difficulty 7 check.", "Small amount of well-hidden cargo space", 1.0, false, 0, false, null, "Smuggler’s hold" },
                    { "Z-88033D0E", "Frigate", 10000, true, "Rather than maintaining lengthy lists of ship equipment, a captain can simply buy an armory. Ships so equipped have whatever amounts of TL4 military-grade weaponry and armor that a crew might require, and integral maintenance facilities for its upkeep. There is enough gear available to outfit the entire crew for normal use, but giving it away or losing it in use may deplete it", "Weapons and armor for the crew", 0.0, false, 0, false, null, "Armory" },
                    { "Z-8E0B88EC", "Fighter", 20000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-3 rating", 2.0, true, 2, true, null, "Drive-3 upgrade" },
                    { "Z-8FF318B2", "Cruiser", 500000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-6 rating", 4.0, true, 3, true, null, "Drive-6 upgrade" },
                    { "Z-91C4628B", "Fighter", 5000, true, "The ship can be designed to accommodate a larger number of crew or passengers. Extended life support can be fitted multiple times; each time, the maximum crew rating of the ship increases by 100% of its normal maximum. Thus, a free merchant who installs this twice can have a maximum complement of 18 people.", "Doubles maximum crew size", 1.0, true, 1, true, null, "Extended life support" },
                    { "Z-98D138AA", "Frigate", 100000, true, "An extremely rare example of psitech dating from before the Scream, a precognitive nav chamber allows a character with at least Precognition-2 psionic skill to assist in interstellar drills, sensing impending shear alterations before they happen. The navigator automatically succeeds on any spike drill check of difficulty 9 or less. On a failed check, add 2 to the Spike Drive Mishap roll, limiting the potential damage. On drilling in to the destination system, the psychic has expended all Effort in the process.", "Allows a precog to assist in navigation", 0.0, false, 1, false, null, "Precognitive nav chamber" },
                    { "Z-A4FC5932", "Frigate", 10000, true, " Each time this fitting is selected, 10% of the ship’s maximum crew gain access to luxury cabins of a spaciousness sufficient to please a wealthy star-farer. This fitting comes with the usual zero-gee athletic courts, decorative fountains, fine dining, and artistic fittings.", "10% of the max crew get luxurious quarters", 1.0, true, 1, false, null, "Luxury cabins" },
                    { "Z-B2A1E145", "Fighter", 0, false, "Free mass can be traded for pressurized cargo space. Tracked by weight for convenience, one cubic meter is usually one ton, with most vehicles requiring ten tons when loaded, tanks taking 25, and aircraft or mechs taking up 50 tons of cargo space. One point of free mass grants 2 tons of cargo space in a fighter, 20 tons in a frigate, 200 tons in a cruiser, and 2000 tons in a capital-class ship. This fitting can be purchased multiple times.", "Pressurized cargo space", 1.0, false, 0, false, null, "Cargo space" },
                    { "Z-B54FEB59", "Frigate", 25000, false, "Cruisers and larger craft can’t land on planetary bodies, so they require small shuttlecraft for transport. A cargo lighter is only capable of surface-to-orbit flight, which takes roughly twenty minutes either way, but can latch on to a standard pressurized cargo container holding up to 200 tons of cargo and passengers. These containers are usually collapsible and take up no significant space when compressed for storage, assuming they’re not simply disposable cargo shells. This fitting can be purchased multiple times.", "Orbit-to-surface cargo shuttle", 2.0, false, 0, false, null, "Cargo lighter" },
                    { "Z-BF5BC6AC", "Fighter", 25000, true, "This fitting includes the benefits of the Atmospheric Configuration fitting, as well as allowing the ship to operate while immersed in a liquid medium. Submerged ships cannot be detected with conventional planetary traffic sensors and require military sonar and naval sensors to fix their position, resources often unavailable on less developed worlds. So long as the ship stays away from military naval craft and bases, it is almost impossible to track while submerged. Only fighter and frigate hull classes can mount this fitting.", "Can land and can operate under water", 1.0, true, 1, false, null, "Amphibious operation" },
                    { "Z-C4BDF244", "Frigate", 10000, true, "Forging new spike courses is too much an art to rely on computerized assistance, but an advanced nav computer can help on well-mapped routes. When navigating an interstellar drill course with charts less than a year old, the navigator decreases drill difficulty by 2.", "Adds +2 for traveling familiar spike courses", 0.0, false, 1, true, null, "Advanced nav computer" },
                    { "Z-C5E1C5EC", "Frigate", 2500, true, "The ship has been fitted with specialized mounts, bunking, cargo bays, and other facilities to expedite the transport of often-unwieldy vehicles, including mechs and other military craft. Any vehicles carried count as only half their usual cargo tonnage. Assuming trained operators, up to four vehicles can be offloaded or ramped onto the ship per round in cases where speed is critical.", "Halve tonnage space of carried vehicles", 1.0, true, 0, false, null, "Vehicle transport fittings" },
                    { "Z-C87BFEF5", "Frigate", 5000, true, " Armored tubes equipped with laser cutter apertures can be used to forcibly invade a hostile ship, provided the target’s engines have been disabled. Ships without boarding tubes have to send invaders across empty space to either make an assault on a doubtless heavily-guarded airlock or cut their way in through the hull with laser cutters and half an hour of work.", "Allows boarding of a hostile disabled ship", 1.0, false, 0, false, null, "Boarding tubes" },
                    { "Z-C9BC4B8A", "Frigate", 10000, true, "Gravitic projectors allow the ship to manipulate objects within its immediate area, pushing, pulling, and sliding objects no larger than a ship of one hull class smaller. Targets with shipscale propulsion can resist the beams, but lifeboats, individual suit jets, or other smaller propulsors are inadequate. The beam can focus on only one object at a time but can move it into the ship’s cargo bays or hurl it out of the ship’s immediate vicinity within three rounds. The beams can only effectively manipulate objects in very low gravity, and not those on the surface of planets or other objects with significant natural gravity.", "Manipulate objects in space at a distance", 1.0, false, 2, false, null, "Tractor beams" },
                    { "Z-D943B0B6", "Frigate", 5000, true, "Fuel scoops allow for the harvesting and extraction of hydrogen from gas giants or the penumbra of solar bodies. The extraction process requires four days of processing and refinement, but completely refuels the ship. Such fittings are common on explorer craft that cannot expect to find refueling stations.", "Ship can scoop fuel from a gas giant or star", 1.0, true, 2, false, null, "Fuel scoops" },
                    { "Z-DB9AF077", "Frigate", 40000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-4 rating", 3.0, true, 2, true, null, "Drive-4 upgrade" },
                    { "Z-DCBE5A06", "Frigate", 100000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-5 rating", 3.0, true, 3, true, null, "Drive-5 upgrade" },
                    { "Z-DFEFD552", "Frigate", null, false, "This pretech relic is unavailable on the open market in most sectors. Once installed in a ship, it can be “imprinted” by up to a dozen psychics. These psychics can sense and affect any object or location within ten meters of the relic after one minute of concentration, provided they are within the same solar system. Thus, teleporters can always teleport next to the anchorpoint, telepaths can always contact anyone within the affected zone, telekinetics can manipulate objects near the relic, and so forth. The relic can be recoded to eliminate existing imprints, but any psychic who gets access to the relic for ten minutes of meditation and focus can imprint on it afterwards. There is no obvious way to tell how many psychics have imprinted to the anchorpoint.", "Focal point for allied psychics’ powers", 0.0, false, 3, false, null, "Psionic anchorpoint" },
                    { "Z-EB708F89", "Frigate", 25000, true, "The ship is designed with symbiosis mounts that allow other ships to “hitch” on the craft’s spike drills. Each shiptender mount allows one craft of a hull size smaller than the tender to link up for intersystem drills. Mounts cannot be used for in-system travel. If the linking ship has been designed to be carried by a tender then establishing this link takes one hour. If not, it takes a full day to fit the ship into the mount. Ships can dismount from the tender with an hour’s disentanglement. In an emergency, the carried ships can dismount instantly, but the mountings are considered disabled then until repairs are made. Carried ships cannot fight.", "Allow another ship to hitch on a spike drive", 1.0, false, 1, false, null, "Shiptender mount" },
                    { "Z-F2B0D2E1", "Frigate", 100000, true, "The ship has been designed to act as the core of a future settlement. Once this fitting is engaged, the ship ceases to be operational as a starship, and builds out into a set of habitats, hydroponic gardens, fusion plants, and living spaces sufficient to support up to five times its maximum crew, including enough fabrication and workshop facilities to keep the settlement operational under normal conditions. The settlement can be a deep-space hab, orbital installation, or planetary settlement. In the latter case, the ship must land to form the settlement; even ships without atmospheric operations can do so, but they can never take off again. Once activated, a colony core cannot be re-packed.", "Ship can be deconstructed into a colony base", 2.0, true, 4, false, null, "Colony core" },
                    { "Z-FB4BD9D0", "Cruiser", 10000, true, "Some ships are designed to produce food and air supplies for the crew. Selecting hydroponic production allows for the indefinite supply of a number of crewmen equal to the ship’s maximum crew. This option may be taken multiple times for farm ships, in which case each additional selection doubles the number of people the ship can support.", "Ship produces life support resources", 2.0, true, 1, true, null, "Hydroponic production" }
                });

            migrationBuilder.InsertData(
                table: "ShipHull",
                columns: new[] { "Id", "Ac", "Armor", "Class", "Cost", "CrewMax", "CrewMin", "Description", "Hardpoints", "Hp", "Mass", "Power", "Speed", "Type" },
                values: new object[,]
                {
                    { "Z-2B2E6C2D", 16, 5, "Fighter", 200000, 1, 1, "Small craft, often modified to replace the spike drive with a system drive. Their speed, cheapness, and combat utility make them a popular choice as inexpensive system patrol craft.", 1, 8, 2, 5, 5, "Strike Fighter" },
                    { "Z-3C8DB61A", 14, 2, "Frigate", 500000, 6, 1, "A hull type much beloved by adventurers, a free merchant has unimpressive combat utility but can carry substantial amounts of cargo while mounting enough weaponry to discourage small-craft piracy.", 2, 20, 15, 10, 3, "Free Merchant" },
                    { "Z-4158AA72", 11, 0, "Fighter", 200000, 10, 1, "The smallest craft that’s regularly used for interstellar drills, a shuttle is a cheap means of moving small amounts of precious material or important persons between worlds.", 1, 15, 5, 3, 3, "Shuttle" },
                    { "Z-4D08885D", 11, 5, "Cruiser", 5000000, 200, 20, "Almost every space-faring world has at least one space station in orbit. A station has no Speed score, no spike drive, and cannot perform any maneuvers in combat, though its transit jets can slowly move it around a solar system over a matter of weeks. Civilian trade stations allow for docking by bulk freighters and ships not cleared to land on the surface, while military stations strictly forbid any civilian docking.", 10, 120, 40, 50, null, "Small Station" },
                    { "Z-5CEF0AF6", 17, 20, "Capital", 40000000, 1000, 100, "Almost every space-faring world has at least one space station in orbit. A station has no Speed score, no spike drive, and cannot perform any maneuvers in combat, though its transit jets can slowly move it around a solar system over a matter of weeks. Civilian trade stations allow for docking by bulk freighters and ships not cleared to land on the surface, while military stations strictly forbid any civilian docking.", 30, 120, 75, 125, null, "Large Station" },
                    { "Z-622389B5", 15, 10, "Frigate", 7000000, 120, 30, "The heaviest starship that most poor or resource-deprived worlds can build, the heavy frigate can carry a significant loadout of weaponry and has enough crew to overwhelm most pirate ships if it comes to a boarding action. While it packs a substantial punch, it lacks the armor of a true cruiser-class warship.", 8, 50, 20, 25, 1, "Heavy Frigate" },
                    { "Z-6944D070", 14, 10, "Capital", 60000000, 1500, 300, "The queen of a fleet, even fewer polities can afford to build one of these huge ships. Carriers can support flights of fighter or frigate-class warships, ones specially equipped to handle particular missions. This versatility allows it to load fighter-bombers for anti-capital missions one month, and then switch to swarms of hunter-killer frigates the next when a hostile system’s asteroid outposts need to be destroyed. Stripped of its combat wings, however, a carrier has less individual firepower than a cruiser.", 4, 75, 100, 50, 0, "Carrier" },
                    { "Z-6FE4E276", 11, 0, "Cruiser", 5000000, 40, 10, "This class of huge cargo ship is found most often in peaceful, heavily-populated sectors.", 2, 40, 25, 15, 0, "Bulk Freighter" },
                    { "Z-B934B650", 14, 15, "Cruiser", 10000000, 200, 50, "The favored ship of the line of most wealthy, advanced worlds, and often the biggest and most powerful ship most planets can build. A cruiser’s heavy armor and infrastructural support for heavy guns make it a lethal weapon against frigates and any other ship not optimized for cracking heavy armor. They can prove vulnerable to swarm attacks by fighter-bombers equipped with the right kind of armor-piercing weapons.", 10, 60, 30, 50, 1, "Fleet Cruiser" },
                    { "Z-BCFA4D84", 16, 20, "Capital", 50000000, 1000, 200, " Dreaded hulks of interstellar war, very few worlds have the necessary technology or economy to support the massive expense of building and crewing a battleship. Those that do gain access to a ship that is largely invulnerable to anything short of specially-designed anti-capital cruisers or hunter-killer frigates.", 15, 100, 50, 75, 0, "Battleship" },
                    { "Z-E238CDAE", 13, 10, "Frigate", 4000000, 40, 10, "The smallest true combat frigate, and often simply called a “frigate” by spacers. Corvettes have significantly thicker armor than patrol boats and trade additional crew needs and less maneuverability for more available free mass.", 6, 40, 15, 15, 2, "Corvette" },
                    { "Z-F3A05A80", 14, 5, "Frigate", 2500000, 20, 5, "The hull of choice for customs cutters and system law enforcement, the patrol boat is a light frigate built heavy enough to overawe small merchant vessels while still being relatively cheap to build and crew.", 4, 25, 10, 15, 4, "Patrol Boat" }
                });

            migrationBuilder.InsertData(
                table: "ShipWeapon",
                columns: new[] { "Id", "AmmoCost", "Class", "Cost", "Description", "Dmg", "Hardpoints", "Mass", "Power", "Qualities", "ShipId", "TechLevel", "Type" },
                values: new object[,]
                {
                    { "Z-0151289B", null, "Capital", 4000000, "Modulation of the ship’s power core emits a cloak of MES lightning. While larger spike drive craft can shunt the energies away harmlessly, fighter-class ships are almost invariably destroyed if hit.", "1d20", 2, 5, 15, "AP 5, Cloud", null, 4, "Lightning Charge Mantle" },
                    { "Z-0AFA19FC", null, "Fighter", 100000, "Stepped tapping of the spike drive power plant allows for the emission of a torrent of charged particles. The particles have very little armor penetration, but can fry a small ship’s power grid in a strike or two.", "3d4", 1, 1, 4, "Clumsy", null, 4, "Reaper battery" },
                    { "Z-20FCC42E", null, "Frigate", 800000, "A focalized upgrade to the reaper battery, the CPC has a much better armor penetration profile.", "3d6", 2, 1, 10, "AP 16, Clumsy", null, 4, "Charged Particle Caster" },
                    { "Z-3566413D", null, "Fighter", 2000000, "A rare example of pretech weaponry, a fighter equipped with a PMB can scratch even a battleship’s hull.", "2d4", 1, 1, 5, "AP 25", null, 5, "Polyspectral MES Beam" },
                    { "Z-3FAFB8A9", null, "Fighter", 50000, "Projecting a spray of tiny, dense particulate matter, sandthrowers are highly effective against lightly-armored fighters.", "2d4", 1, 1, 3, "Flak", null, 4, "Sandthrower" },
                    { "Z-501FB87C", null, "Cruiser", 2500000, "The SIP uses the ship’s spike phasing as an offensive weapon, penetrating the target with a brief incursion of MES energies that largely ignore attempts to evade.", "3d8", 3, 3, 10, "AP 15", null, 4, "Spike Inversion Projector" },
                    { "Z-63BDE671", 500, "Fighter", 200000, "A spray of penetrator sabots that use fractal surfacing to increase impact. Favored for bomber-class fighter hulls.", "2d6", 1, 1, 5, "AP 15, Ammo 4", null, 4, "Fractal Impact Charge" },
                    { "Z-7F03F812", null, "Fighter", 100000, "Twinned assay and penetration lasers modulate the frequency of this beam for remarkable armor penetration. These weapons are popular choices for fighters intended for frigate or cruiser engagement.", "1d4", 1, 1, 5, "AP 20", null, 4, "Multifocal Laser" },
                    { "Z-8EFCAD39", null, "Frigate", 500000, "A baseline frigate anti-fighter system, this battery fires waves of lasers or charged particles to knock down small craft.", "2d6", 1, 3, 5, "AP 10, Flak", null, 4, "Flak Emitter Battery" },
                    { "Z-A2DE5245", null, "Cruiser", 2000000, "A swarm of self-directed microdrones sweeps over the ship. Their integral beam weaponry is too small to damage larger ships, but they can wipe out an attacking fighter wave.", "3d10", 2, 5, 10, "Cloud, Clumsy", null, 4, "Smart Cloud" },
                    { "Z-A708C86F", null, "Capital", 20000000, "One of the few surviving pretech weapons in anything resembling wide currency, this capital-class weapons system fires something mathematically related to a miniaturized black hole at a target.", "5d20", 5, 10, 25, "AP 25", null, 5, "Singularity Gun" },
                    { "Z-A82033D9", null, "Cruiser", 1500000, "One of the first spinal-mount class weapons, the SBC briefly channels the full power of the ship into a charged beam. It lacks the power and penetration of the more advanced gravcannon, but also takes less power to mount.", "3d10", 3, 5, 10, "AP 15, Clumsy", null, 4, "Spinal Beam Cannon" },
                    { "Z-B130378B", null, "Frigate", 700000, "With superior targeting and a smaller energy drain than a CPC, a plasma beam sacrifices some armor penetration.", "3d6", 2, 2, 5, "AP 10", null, 4, "Plasma Beam" },
                    { "Z-C7C1FFF4", 5000, "Frigate", 50000, "Useless in ship-to-ship combat or against any other TL4 planet with working nuke snuffers, one of these missiles can still erase an entire lostworlder city without such protection.", "Special", 2, 1, 5, "Ammo 5", null, 4, "Nuclear Missiles" },
                    { "Z-E35FA631", 5000, "Frigate", 1000000, "A storm of magnetically-accelerated spike charges is almost guaranteed to eradicate any fighter-class craft it hits.", "2d6+2", 2, 2, 5, "Flak, AP 10, Ammo 5", null, 4, "Mag Spike Array" },
                    { "Z-E9172091", null, "Capital", 5000000, "A capital-class model of the SIP, a VTI is capable of incapacitating a cruiser in two hits. Its bulk limits its utility against fighter-class craft, however.", "3d20", 4, 10, 20, "AP 20, Clumsy", null, 4, "Vortex Tunnel Inductor" },
                    { "Z-EAAB4EED", 2500, "Frigate", 500000, "Capable of damaging even a battleship, torpedoes are cumbersome, expensive, and often the core of a line frigate’s armament", "3d8", 1, 3, 10, "AP 20, Ammo 4", null, 4, "Torpedo Launcher" },
                    { "Z-F77BA026", null, "Cruiser", 2000000, "Using much the same principles as man-portable grav weaponry, the gravcannon causes targets to fall apart in a welter of mutually-antagonistic gravitic fields.", "4d6", 3, 4, 15, "AP 20", null, 4, "Gravcannon" },
                    { "Z-F933543D", 50000, "Capital", 5000000, "By firing projectiles almost as large as fighter-scale craft, the mass cannon inflicts tremendous damage on a target. Serious ammunition limitations hamper its wider-scale use.", "2d20", 4, 5, 10, "AP 20, Ammo 4", null, 4, "Mass Cannon" }
                });

            migrationBuilder.InsertData(
                table: "Universes",
                columns: new[] { "Id", "GridX", "GridY", "Name" },
                values: new object[] { "UN-288B1A00", 5, 5, "AutoInsert" });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_BirthPlanetId",
                table: "Characters",
                column: "BirthPlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UniverseId",
                table: "Characters",
                column: "UniverseId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewMember_CharacterId",
                table: "CrewMember",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrewMember_ShipId",
                table: "CrewMember",
                column: "ShipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planets_ZoneId",
                table: "Planets",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsOfInterest_UniverseId",
                table: "PointsOfInterest",
                column: "UniverseId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsOfInterest_ZoneId",
                table: "PointsOfInterest",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipDefense_ShipId",
                table: "ShipDefense",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipFitting_ShipId",
                table: "ShipFitting",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_HullId",
                table: "Ships",
                column: "HullId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_UniverseId",
                table: "Ships",
                column: "UniverseId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipWeapon_ShipId",
                table: "ShipWeapon",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_UniverseId",
                table: "Stars",
                column: "UniverseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_ZoneId",
                table: "Stars",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_UniverseId",
                table: "Zones",
                column: "UniverseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrewMember");

            migrationBuilder.DropTable(
                name: "PointsOfInterest");

            migrationBuilder.DropTable(
                name: "Problems");

            migrationBuilder.DropTable(
                name: "ShipDefense");

            migrationBuilder.DropTable(
                name: "ShipFitting");

            migrationBuilder.DropTable(
                name: "ShipWeapon");

            migrationBuilder.DropTable(
                name: "Stars");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "ShipHull");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Universes");
        }
    }
}
