using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SWNUniverseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_ShipHullObject_HullId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_HullId",
                table: "Ships");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-017CD85F");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-07844F08");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-0D67C59F");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-3FB87475");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-404CFB0D");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-45B760DD");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-A203AA4C");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-B567FE10");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-C1411811");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-0567568C");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-0C4010FF");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-123FAADB");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-1A13F862");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-1AA91E37");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-1D46B008");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-1ED7CA25");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-285CD043");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-2A772E8D");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-32E1DF63");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-39EB539C");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-3D0864B3");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-3EB7A598");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-47519D08");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-543A3B18");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-5A52AA98");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-620A2D04");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-69F82B04");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-6BDF2B43");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-6D56D5E0");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-7412C698");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-79679EB6");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-7FDF1B28");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-80B3847E");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-87E19910");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-933CFAB1");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-9BC5C03A");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-9D6FA079");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-9E34B27A");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-A2538CF5");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-A6905540");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-A7D06CD7");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-AB2D2C47");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-B306D6E6");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-B62E469C");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-C3AF7E43");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-C5C60697");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-C74686AD");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-C79799B4");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-C81466A5");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-CB58A963");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-D24E35D9");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-DB3A5684");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-E494F028");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-EA14873B");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-F9381343");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-10BE462F");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-2054C4DB");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-394B13F4");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-48769B70");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-62DF9C6C");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-6F79B731");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-89B40594");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-8B63A634");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-B7A240C3");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-BE931FB4");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-D13E4100");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-F3DB144D");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-0A3FCC41");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-108735D7");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-1CE94599");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-1E5B1E13");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-22324E8E");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-279F586A");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-43D0FA6A");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-4D39338F");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-4E1DDD40");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-60A9D83F");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-81180EB2");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-8B9D3AC0");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-8D2C547D");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-8FDCE1ED");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-9D523546");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-BAB6A48B");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-D5A00BAD");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-E9AB7A1B");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-F86B7EE9");

            migrationBuilder.AlterColumn<string>(
                name: "HullId",
                table: "Ships",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "ShipDefense",
                columns: new[] { "Id", "Class", "Cost", "CostExtra", "Description", "Effect", "Mass", "MassExtra", "Power" },
                values: new object[,]
                {
                    { "Z-009893FC", "Fighter", 25000, true, "A complex glazing process can harden the surface of a ship’s armor to more effectively shed incoming attacks, decreasing the armor-piercing quality of any hit by 5.", "AP quality of attacking weapons reduced by 5", 1, true, 0 },
                    { "Z-0E7E6633", "Frigate", 10000, true, "Too small to damage ships, these point defense lasers can detonate or melt incoming munitions, improving the ship’s defenses against torpedoes, fractal impact charges, and other ammunition-based weapons.", "+2 AC versus weapons that use ammo", 2, true, 3 },
                    { "Z-0E846AC2", "Fighter", 25000, true, "At the cost of a certain amount of speed and maneuverability, a ship can have its armor plating reinforced against glancing hits, gaining a +2 bonus to its AC. This augmentation can decrease a ship’s Speed below 0, meaning it will be applied as a penalty to all Pilot tests.", "+2 AC, -1 speed", 1, true, 0 },
                    { "Z-39BAAA8F", "Frigate", 25000, true, "A high-powered secondary ECM generator can be activated to negate any one otherwise-successful hit against the ship. This generator can be activated after the damage has been rolled, but enemy ships rapidly compensate for the new ECM source, and so the generator can only be used effectively once per engagement", "Negate one successful hit", 1, true, 2 },
                    { "Z-5D23908A", "Frigate", 50000, true, "The ship is equipped with an array of gravitic braker guns and an upgraded nuke snuffer field. While useless in conventional ship-to-ship combat, the array can deflect or dampen meteor impacts, dropped penetrator rods, or other non-powered bombardment techniques, and the snuffer field is powerful enough to prevent nuclear fission reactions over a hemisphere-sized area. A single ship with a PDA can protect against any natural meteor strikes and deny easy terror bombardment of a planet’s population. The PDA cannot fully protect against orbital strikes by powered penetrators, but it can nudge them off course and make pinpoint strikes impractical. Most developed planets have much more powerful and effective ground installations, but a PDA-equipped ship is a useful emergency stopgap for poor or primitive worlds.", "Anti-impact and anti-nuke surface defenses", 2, true, 4 },
                    { "Z-86BE2F61", "Capital", 100000, true, "By sacrificing empty hull space in a complex system of ablative blast baffles, a capital-class ship can have a large amount of its total mass shot away without actually impinging on its normal function. This grants it a +1 AC bonus and 20 extra maximum hit points.", "+1 AC, +20 maximum hit points", 2, true, 5 },
                    { "Z-8996B648", "Frigate", 50000, true, "This system links with a ship’s navigational subsystem and randomizes the motion vectors in sympathy with metadimensional gravitic currents. This agility gives any hit on the ship a 1 in 6 chance of being negated entirely.", "1 in 6 change of any given attack missing", 2, true, 5 },
                    { "Z-E85B9E7C", "Cruiser", 10000, true, "These drones are invariably short-lived due to the enormous energy signatures they produce, but until the ship’s next turn they grant a +2 AC bonus as their emissions confuse foes. Foxer drones are cheaply constructed and essentially free; the only limit on their number is the amount of free space set aside for holding them.", "+2 AC for one round when fired, ammo 5", 1, true, 2 },
                    { "Z-EFF138B8", "Frigate", 25000, true, "The ship has hardened bulkheads, reinforced hatches, and specially-designed automated kill corridors for wiping out intruders. Provided that the ship’s bridge is still under control, the operator can prevent entry to the ship by any force that lacks shipyard-grade tools, weapons capable of melting hull plating, or specialized military breaching implements. If intruders do get inside, only well-equipped, specially-trained marines have any real chance of breaching the defenses. Ordinary space pirates or more casual invaders have only a 1 in 6 chance of threatening the bridge crew, though they may cause significant damage in their dying.", "Makes enemy boarding more difficult", 1, true, 2 }
                });

            migrationBuilder.InsertData(
                table: "ShipFittingObject",
                columns: new[] { "Id", "Class", "Cost", "CostExtra", "Description", "Effect", "Mass", "MassExtra", "Power", "PowerExtra", "Type" },
                values: new object[,]
                {
                    { "Z-0151458F", "Frigate", 25000, false, "Cruisers and larger craft can’t land on planetary bodies, so they require small shuttlecraft for transport. A cargo lighter is only capable of surface-to-orbit flight, which takes roughly twenty minutes either way, but can latch on to a standard pressurized cargo container holding up to 200 tons of cargo and passengers. These containers are usually collapsible and take up no significant space when compressed for storage, assuming they’re not simply disposable cargo shells. This fitting can be purchased multiple times.", "Orbit-to-surface cargo shuttle", 2.0, false, 0, false, "Cargo lighter" },
                    { "Z-04AFB664", "Frigate", 500, true, "On-board workshops can be bought at smaller than maximum sizes than a hull would allow, if additional TL4 tech facilities aren’t strictly needed. A frigate-sized workshop is sufficient for modding any personal gear as per the modification rules on page 100. A cruiser-sized workshop can handle vehicle modding and starship maintenance, and a capital-class workshop can build vehicles or similar large work from scratch at full cost.", "Automated tech workshops for maintenance", 0.5, true, 1, false, "Workshop" },
                    { "Z-06D61104", "Cruiser", 500000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-6 rating", 4.0, true, 3, true, "Drive-6 upgrade" },
                    { "Z-0D3828AD", "Cruiser", 50000, true, "These banked rows of compressed cold sleep pods are designed to carry enormous numbers of people in extended hibernation, most often colonists to some new homeworld or escapees from some stellar disaster. A cruiser can carry up to 1,000 colonists in stasis, while a capital ship can handle up to 5,000. Each further time this fitting is selected, these numbers double. These pods put their inhabitants into very deep hibernation so as to minimize the resources necessary to maintain their lives. Bringing them out of this stasis requires a month of “defrost”. Crash awakenings have a 25% chance of killing the subject. The pods are rated for 100 years of stasis, but their actual maximum duration is somewhat speculative.", "House vast numbers of cold sleep passengers", 2.0, true, 1, true, "Exodus bay" },
                    { "Z-0D795AD2", "Fighter", 20000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-3 rating", 2.0, true, 2, true, "Drive-3 upgrade" },
                    { "Z-10F1CD90", "Fighter", 0, false, "Free mass can be traded for pressurized cargo space. Tracked by weight for convenience, one cubic meter is usually one ton, with most vehicles requiring ten tons when loaded, tanks taking 25, and aircraft or mechs taking up 50 tons of cargo space. One point of free mass grants 2 tons of cargo space in a fighter, 20 tons in a frigate, 200 tons in a cruiser, and 2000 tons in a capital-class ship. This fitting can be purchased multiple times.", "Pressurized cargo space", 1.0, false, 0, false, "Cargo space" },
                    { "Z-135B5D6B", "Fighter", 25000, true, "Stealth systems can mask the ship’s energy emissions through careful modulation of the output. All travel times inside a star system are doubled when the system is engaged, but any skill checks to avoid detection gain a +2 bonus.", "Adds +2 to skill checks to avoid detection", 1.0, true, 1, true, "Emissions dampers" },
                    { "Z-291BAC19", "Frigate", 10000, true, "A lab suitable for investigating alien xenolife, planetary geology, esoteric technology, and other mysteries of the cosmos. The lab contains cold sleep pods adequate to contain alien samples, viro-shielded research cells, high-energy lasers, and other requisite tools. When used to investigate some phenomenon or object, any applicable skill rolls are improved by +1 for a frigate lab, +2 for a cruiser lab, and +3 for a capital ship lab.", "Skill bonus for analysis and research", 2.0, false, 1, true, "Advanced lab" },
                    { "Z-2B0D9E5A", "Frigate", 10000, true, " Each time this fitting is selected, 10% of the ship’s maximum crew gain access to luxury cabins of a spaciousness sufficient to please a wealthy star-farer. This fitting comes with the usual zero-gee athletic courts, decorative fountains, fine dining, and artistic fittings.", "10% of the max crew get luxurious quarters", 1.0, true, 1, false, "Luxury cabins" },
                    { "Z-2E44F3AA", "Frigate", null, false, "This pretech relic is unavailable on the open market in most sectors. Once installed in a ship, it can be “imprinted” by up to a dozen psychics. These psychics can sense and affect any object or location within ten meters of the relic after one minute of concentration, provided they are within the same solar system. Thus, teleporters can always teleport next to the anchorpoint, telepaths can always contact anyone within the affected zone, telekinetics can manipulate objects near the relic, and so forth. The relic can be recoded to eliminate existing imprints, but any psychic who gets access to the relic for ten minutes of meditation and focus can imprint on it afterwards. There is no obvious way to tell how many psychics have imprinted to the anchorpoint.", "Focal point for allied psychics’ powers", 0.0, false, 3, false, "Psionic anchorpoint" },
                    { "Z-320B836F", "Fighter", 50000, false, "Some ships run with more guns than crewmen. A single NPC or PC can man one gun per round, firing it as often as the gunnery chief’s Fire One Weapon or Fire All Guns actions allow, but sometimes that’s not enough. Installing an autonomic targeting system for a gun allows it to shoot at a +2 hit bonus without human assistance. This system must be installed once for each gun that is to be self-manned.", "Fires one weapon system without a gunner", 0.0, false, 1, false, "Auto-targeting system" },
                    { "Z-32F47F17", "Frigate", 100000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-5 rating", 3.0, true, 3, true, "Drive-5 upgrade" },
                    { "Z-3FAB2AE9", "Fighter", 2500, true, "A normal complement of ship’s stores can keep the maximum crew size supplied for two months. Each selection of Extended Stores doubles that time, and can be fitted multiple times.", "Maximum life support duration is doubled", 1.0, true, 0, false, "Extended stores" },
                    { "Z-4E8FAC56", "Capital", 1000000, false, "These sophisticated docking bays provide all the necessary tools and support for launching a starship from the mother craft. They are rarely seen outside of dedicated capital-class carriers, but some cruisers make room to mount a fighter-class attack shuttle. Each bay allows room for one ship of the appropriate hull class. While the carried ship can support its own crew if necessary, most carriers fold their space wing into the mothership’s crew roster. This fitting can be taken multiple times.", "Carrier housing for a frigate", 4.0, false, 1, false, "Ship bay/frigate" },
                    { "Z-63400B4F", "Fighter", 5000, true, "The ship can be designed to accommodate a larger number of crew or passengers. Extended life support can be fitted multiple times; each time, the maximum crew rating of the ship increases by 100% of its normal maximum. Thus, a free merchant who installs this twice can have a maximum complement of 18 people.", "Doubles maximum crew size", 1.0, true, 1, true, "Extended life support" },
                    { "Z-6B550534", "Frigate", 10000, true, "Gravitic projectors allow the ship to manipulate objects within its immediate area, pushing, pulling, and sliding objects no larger than a ship of one hull class smaller. Targets with shipscale propulsion can resist the beams, but lifeboats, individual suit jets, or other smaller propulsors are inadequate. The beam can focus on only one object at a time but can move it into the ship’s cargo bays or hurl it out of the ship’s immediate vicinity within three rounds. The beams can only effectively manipulate objects in very low gravity, and not those on the surface of planets or other objects with significant natural gravity.", "Manipulate objects in space at a distance", 1.0, false, 2, false, "Tractor beams" },
                    { "Z-6D1D6B26", "Frigate", 10000, true, "Forging new spike courses is too much an art to rely on computerized assistance, but an advanced nav computer can help on well-mapped routes. When navigating an interstellar drill course with charts less than a year old, the navigator decreases drill difficulty by 2.", "Adds +2 for traveling familiar spike courses", 0.0, false, 1, true, "Advanced nav computer" },
                    { "Z-72D47DFB", "Fighter", 5000, true, "This fitting must be put in place when the ship is built, and cannot be installed on cruiser-class or larger ships. A ship designed for atmospheric flight can land on most solid or aqueous surfaces.", "Can land: frigates and fighters only.", 1.0, true, 0, false, "Atmospheric configuration" },
                    { "Z-73E39A84", "Frigate", 2500, true, "The ship has been fitted with specialized mounts, bunking, cargo bays, and other facilities to expedite the transport of often-unwieldy vehicles, including mechs and other military craft. Any vehicles carried count as only half their usual cargo tonnage. Assuming trained operators, up to four vehicles can be offloaded or ramped onto the ship per round in cases where speed is critical.", "Halve tonnage space of carried vehicles", 1.0, true, 0, false, "Vehicle transport fittings" },
                    { "Z-74116D4C", "Cruiser", 50000, true, "The ship is equipped with a full-scale TL4 fabrication plant programmed to support its needs. The ship can stock raw materials and parts at 5,000 credits per ton; these parts can then be used to “pay” ship repair or maintenance costs when conventional shipyards are unavailable. The factory can also create and repair vehicles, TL4 equipment, space habs, and planetary structures with these parts at a rate of 10,000 credits worth of construction a day. If a mobile extractor is available, the raw materials processed by the latter unit can be used to feed the factory. Operating a mobile factory requires at least 100 well-trained personnel. Each 10 fewer available doubles repair or maintenance times.", "Self-sustaining factory and repair facilities", 2.0, true, 3, false, "Mobile factory" },
                    { "Z-74B0450B", "Frigate", 25000, true, "Drills along a known spike drive route have no chance of failure so long as the route is no longer than twice the operating pilot’s skill level and does not involve course trimming. Thus, a navigator with Pilot-1 skill would always succeed in making a drill of two hexes or less, provided the route was a known one. The drill course regulator requires a sophisticated technical base and compatible metadimensional energy conditions in a given sector; many sectors no longer have this technology or lack the right environment to use it. If the GM prefers a campaign where space travel is always at least somewhat dangerous, they may deny access to this fitting. Ships piloted by exceptionally talented navigators may choose not to mount it even then.", "Common drill routes become auto-successes", 1.0, false, 1, true, "Drill course regulator" },
                    { "Z-78D28CBC", "Frigate", 5000, true, " Armored tubes equipped with laser cutter apertures can be used to forcibly invade a hostile ship, provided the target’s engines have been disabled. Ships without boarding tubes have to send invaders across empty space to either make an assault on a doubtless heavily-guarded airlock or cut their way in through the hull with laser cutters and half an hour of work.", "Allows boarding of a hostile disabled ship", 1.0, false, 0, false, "Boarding tubes" },
                    { "Z-79A5800D", "Frigate", 5000, true, "Fuel scoops allow for the harvesting and extraction of hydrogen from gas giants or the penumbra of solar bodies. The extraction process requires four days of processing and refinement, but completely refuels the ship. Such fittings are common on explorer craft that cannot expect to find refueling stations.", "Ship can scoop fuel from a gas giant or star", 1.0, true, 2, false, "Fuel scoops" },
                    { "Z-7C0C4782", "Fighter", 2500, true, "Most ships require refueling after each drill jump, no matter the distance. Installing fuel bunkers allows the ship to carry one additional load of fuel. This fitting can be installed multiple times for ships that wish to minimize fueling.", "Adds fuel for one more drill between fuelings", 1.0, false, 0, false, "Fuel bunkers" },
                    { "Z-8A596149", "Frigate", 2500, true, "Selecting this fitting equips the ship with a number of single-use escape craft capable of reaching the nearest habitable planet or station in a star system. If no such destination exists, the boats can maintain their passengers for up to a year in drugged semi-stasis. Lifeboats have fully-functional comm systems and are usually equipped with basic survival supplies and distress beacons. A single selection of this fitting provides enough lifeboats for a ship’s maximum crew, with up to twenty people per boat.", "Emergency escape craft for a ship’s crew", 1.0, false, 0, false, "Lifeboats" },
                    { "Z-8B43469A", "Frigate", 10000, true, "The ship can disguise its long-range sensor readings, spoofing scans with the ID tags and apparent hull type of any other ship of its choice. To penetrate this masquerade, the scanning entity must beat a Wis/Program skill check against difficulty 10 plus the Program skill of the masking ship’s comms officer. Once the ship is close enough to visually identify, the masking is useless.", "At long distances, disguise ship as another", 0.0, false, 1, true, "Sensor mask" },
                    { "Z-8F3BCE5C", "Cruiser", 10000, true, "Some ships are designed to produce food and air supplies for the crew. Selecting hydroponic production allows for the indefinite supply of a number of crewmen equal to the ship’s maximum crew. This option may be taken multiple times for farm ships, in which case each additional selection doubles the number of people the ship can support.", "Ship produces life support resources", 2.0, true, 1, true, "Hydroponic production" },
                    { "Z-97104540", "Frigate", 25000, true, "The ship is designed with symbiosis mounts that allow other ships to “hitch” on the craft’s spike drills. Each shiptender mount allows one craft of a hull size smaller than the tender to link up for intersystem drills. Mounts cannot be used for in-system travel. If the linking ship has been designed to be carried by a tender then establishing this link takes one hour. If not, it takes a full day to fit the ship into the mount. Ships can dismount from the tender with an hour’s disentanglement. In an emergency, the carried ships can dismount instantly, but the mountings are considered disabled then until repairs are made. Carried ships cannot fight.", "Allow another ship to hitch on a spike drive", 1.0, false, 1, false, "Shiptender mount" },
                    { "Z-9A824AD0", "Frigate", 10000, true, "Rather than maintaining lengthy lists of ship equipment, a captain can simply buy an armory. Ships so equipped have whatever amounts of TL4 military-grade weaponry and armor that a crew might require, and integral maintenance facilities for its upkeep. There is enough gear available to outfit the entire crew for normal use, but giving it away or losing it in use may deplete it", "Weapons and armor for the crew", 0.0, false, 0, false, "Armory" },
                    { "Z-A41E6F4A", "Fighter", 0, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "The standard spike drive common among all ships", 0.0, true, 0, true, "Drive-1" },
                    { "Z-A932538E", "Frigate", 2000, true, "Much like an armory, this option allows a captain to lay in a general supply of equipment likely to be useful to explorers and spacemen. Any TL4 equipment on the gear list can be found in the ship’s locker in amounts commensurate with the ship’s size. A few guns and some basic armor might be included as well, but for serious armament an armory is required. There is enough gear available to outfit the entire crew for normal use, but giving it away or losing it in use may deplete the locker until it is restocked.", "General equipment for the crew", 0.0, false, 0, false, "Ship’s locker" },
                    { "Z-AD80BB5A", "Frigate", 50000, false, "Automated mining and refinery equipment has been built into the ship, allowing it to extract resources from asteroids and planetary surfaces. Careful extraction of specific lodes of valuable minerals can be quite profitable at the GM’s discretion, but if the crew is simply melting down available asteroids for raw materials, the unit can refine one ton of usable materials per day worth about 500 credits in most markets. These raw materials can be used to feed a mobile factory, and a ship may have more than one mobile extractor fitted to it to multiply the return if sufficient raw feedstock is available. Operating an extractor requires at least five crew members.", "Space mining and refinery fittings", 1.0, false, 2, false, "Mobile extractor" },
                    { "Z-AE9D3250", "Frigate", 5000, true, "Most ships require only basic analysis of a star system, sufficient to identify population centers, do rough scanning of an object’s composition, and chart major navigational hazards. Survey sensor arrays greatly enhance the ship’s sensor abilities, allowing for finely-detailed mapping of objects and planets, along with broad-spectrum communications analysis. They also improve attempts to detect other craft when scanning a region for stealthy vessels. Any rolls with survey sensor arrays add +2 to skill checks.", "Improved planetary sensory array", 1.0, false, 2, false, "Survey sensor array" },
                    { "Z-AEB24BDC", "Frigate", 100000, true, "An extremely rare example of psitech dating from before the Scream, a precognitive nav chamber allows a character with at least Precognition-2 psionic skill to assist in interstellar drills, sensing impending shear alterations before they happen. The navigator automatically succeeds on any spike drill check of difficulty 9 or less. On a failed check, add 2 to the Spike Drive Mishap roll, limiting the potential damage. On drilling in to the destination system, the psychic has expended all Effort in the process.", "Allows a precog to assist in navigation", 0.0, false, 1, false, "Precognitive nav chamber" },
                    { "Z-B27DC9D9", "Frigate", 5000, true, "All ships are equipped with basic medical facilities for curing lightly injured crew members and keeping the seriously injured ones stable until reaching a planet. An extended medbay improves those facilities, allowing for the medical treatment of up to the ship’s entire maximum crew at once, including the treatment of critically wounded passengers.", "Can provide medical care to more patients", 1.0, false, 1, false, "Extended medbay" },
                    { "Z-BC0C7702", "Cruiser", 200000, false, "These sophisticated docking bays provide all the necessary tools and support for launching a starship from the mother craft. They are rarely seen outside of dedicated capital-class carriers, but some cruisers make room to mount a fighter-class attack shuttle. Each bay allows room for one ship of the appropriate hull class. While the carried ship can support its own crew if necessary, most carriers fold their space wing into the mothership’s crew roster. This fitting can be taken multiple times.", "Carrier housing for a fighter", 2.0, false, 0, false, "Ship bay/fighter" },
                    { "Z-C3DA94C7", "Fighter", 10000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-2 rating", 1.0, true, 1, true, "Drive-2 upgrade" },
                    { "Z-C8009882", "Fighter", 10000, true, "The ship has been carefully fitted to support the use of non-sentient expert system robots in its operation. At least one human, VI, or True AI crew member is necessary to oversee the bots and monitor spike drills, but otherwise crew may be replaced with cheap, basic robots at a cost of 1,000 credits per crew member replaced. Bots don’t draw pay, don’t take up life support, and their maintenance is assumed to be part of the ship’s operating costs. These bots are incapable of any actions unrelated to operating the ship and are treated as level-0 in their skills where relevant.", "Ship can use simple robots as crew", 1.0, false, 2, false, "Automation support" },
                    { "Z-D0AF1677", "Fighter", null, false, "The ship’s standard drive-1 spike drive is removed and replaced with a different propulsion system. The ship is still treated as having a drive-1 for maneuvering and system transit purposes, but it cannot make interstellar drills. This modification lowers the cost of the basic hull by 10% and adds extra power and space based on the size of the hull: 1 power for fighters, 2 for frigates, 3 for cruisers, and 4 for capital ship hulls. Twice this amount of free mass is gained by the process.", "Replace spike drive with small system drive", -2.0, true, -1, true, "System drive" },
                    { "Z-D4B40E85", "Fighter", 2500, true, " Carefully-designed storage space intended to conceal illicit cargo from customs inspection. Each installation of this fitting adds 200 kilograms of cargo space in a fighter, 2 tons in a frigate, 20 tons in a cruiser, or 200 tons in a capital ship. Cargo in a smuggler’s hold will never be found by a standard customs inspection. Careful investigation by a suspicious official can find it on a difficulty 10 check using their Wis/Notice skill, and a week-long search one step short of disassembly will find it on a difficulty 7 check.", "Small amount of well-hidden cargo space", 1.0, false, 0, false, "Smuggler’s hold" },
                    { "Z-DA327974", "Frigate", 5000, true, "These stasis pods can keep a subject alive for centuries provided that the ship’s power doesn’t fail. Each installation allows for keeping a number of people equal to the ship’s maximum crew in stasis indefinitely. This fitting can be installed multiple times.", "Keeps occupants in stasis", 1.0, false, 1, false, "Cold sleep pods" },
                    { "Z-E4C59176", "Frigate", 100000, true, "The ship has been designed to act as the core of a future settlement. Once this fitting is engaged, the ship ceases to be operational as a starship, and builds out into a set of habitats, hydroponic gardens, fusion plants, and living spaces sufficient to support up to five times its maximum crew, including enough fabrication and workshop facilities to keep the settlement operational under normal conditions. The settlement can be a deep-space hab, orbital installation, or planetary settlement. In the latter case, the ship must land to form the settlement; even ships without atmospheric operations can do so, but they can never take off again. Once activated, a colony core cannot be re-packed.", "Ship can be deconstructed into a colony base", 2.0, true, 4, false, "Colony core" },
                    { "Z-E7ACDC8D", "Frigate", null, false, "This TL5 tech is completely unavailable in most sectors and was uncommon even on Mandate-era ships. The pads allow up to a dozen people or 1,200 kilograms of matter to be teleported to and from the surface of a planet or the interior of a another ship, provided it is no more than a few tens of thousands of kilometers distant. Ship-to-ship teleportation is possible only when the receiving ship is cooperating by transmitting accurate coordinate details; otherwise, a friendly, unjammed signal from inside is necessary to lock onto the target point. Planetary teleportations are possible only in the absence of physical barriers between the ship and the target point below. The teleporters may be used once every five minutes. In those rare sectors where this tech is widely available, it costs 200k.", "Pretech teleportation to and from ship", 1.0, false, 1, false, "Teleportation pads" },
                    { "Z-EBCB60A3", "Fighter", 25000, true, "This fitting includes the benefits of the Atmospheric Configuration fitting, as well as allowing the ship to operate while immersed in a liquid medium. Submerged ships cannot be detected with conventional planetary traffic sensors and require military sonar and naval sensors to fix their position, resources often unavailable on less developed worlds. So long as the ship stays away from military naval craft and bases, it is almost impossible to track while submerged. Only fighter and frigate hull classes can mount this fitting.", "Can land and can operate under water", 1.0, true, 1, false, "Amphibious operation" },
                    { "Z-F4E71A51", "Frigate", 300000, false, "Armored and stealthed versions of cargo lighters, these craft are twice as fast, apply a -3 penalty to tracking and targeting skill checks, and can carry up to one hundred troops or passengers. Many are equipped with assorted Heavy weapons to clear the landing zone, and can be treated as flight-capable gravtanks for purposes of combat. This fitting can be purchased multiple times.", "Stealthed landing pod for troops", 2.0, false, 0, false, "Drop pod" },
                    { "Z-F95107AB", "Frigate", 40000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-4 rating", 3.0, true, 2, true, "Drive-4 upgrade" }
                });

            migrationBuilder.InsertData(
                table: "ShipHullObject",
                columns: new[] { "Id", "Ac", "Armor", "Class", "Cost", "CrewMax", "CrewMin", "Description", "Hardpoints", "Hp", "Mass", "Power", "Speed", "Type" },
                values: new object[,]
                {
                    { "Z-07CFCF2D", 15, 10, "Frigate", 7000000, 120, 30, "The heaviest starship that most poor or resource-deprived worlds can build, the heavy frigate can carry a significant loadout of weaponry and has enough crew to overwhelm most pirate ships if it comes to a boarding action. While it packs a substantial punch, it lacks the armor of a true cruiser-class warship.", 8, 50, 20, 25, 1, "Heavy Frigate" },
                    { "Z-19405010", 17, 20, "Capital", 40000000, 1000, 100, "Almost every space-faring world has at least one space station in orbit. A station has no Speed score, no spike drive, and cannot perform any maneuvers in combat, though its transit jets can slowly move it around a solar system over a matter of weeks. Civilian trade stations allow for docking by bulk freighters and ships not cleared to land on the surface, while military stations strictly forbid any civilian docking.", 30, 120, 75, 125, null, "Large Station" },
                    { "Z-346F4C9C", 14, 5, "Frigate", 2500000, 20, 5, "The hull of choice for customs cutters and system law enforcement, the patrol boat is a light frigate built heavy enough to overawe small merchant vessels while still being relatively cheap to build and crew.", 4, 25, 10, 15, 4, "Patrol Boat" },
                    { "Z-3BD955C8", 14, 10, "Capital", 60000000, 1500, 300, "The queen of a fleet, even fewer polities can afford to build one of these huge ships. Carriers can support flights of fighter or frigate-class warships, ones specially equipped to handle particular missions. This versatility allows it to load fighter-bombers for anti-capital missions one month, and then switch to swarms of hunter-killer frigates the next when a hostile system’s asteroid outposts need to be destroyed. Stripped of its combat wings, however, a carrier has less individual firepower than a cruiser.", 4, 75, 100, 50, 0, "Carrier" },
                    { "Z-44409F37", 16, 20, "Capital", 50000000, 1000, 200, " Dreaded hulks of interstellar war, very few worlds have the necessary technology or economy to support the massive expense of building and crewing a battleship. Those that do gain access to a ship that is largely invulnerable to anything short of specially-designed anti-capital cruisers or hunter-killer frigates.", 15, 100, 50, 75, 0, "Battleship" },
                    { "Z-449840F0", 13, 10, "Frigate", 4000000, 40, 10, "The smallest true combat frigate, and often simply called a “frigate” by spacers. Corvettes have significantly thicker armor than patrol boats and trade additional crew needs and less maneuverability for more available free mass.", 6, 40, 15, 15, 2, "Corvette" },
                    { "Z-7D814EEA", 14, 2, "Frigate", 500000, 6, 1, "A hull type much beloved by adventurers, a free merchant has unimpressive combat utility but can carry substantial amounts of cargo while mounting enough weaponry to discourage small-craft piracy.", 2, 20, 15, 10, 3, "Free Merchant" },
                    { "Z-831E2CDD", 14, 15, "Cruiser", 10000000, 200, 50, "The favored ship of the line of most wealthy, advanced worlds, and often the biggest and most powerful ship most planets can build. A cruiser’s heavy armor and infrastructural support for heavy guns make it a lethal weapon against frigates and any other ship not optimized for cracking heavy armor. They can prove vulnerable to swarm attacks by fighter-bombers equipped with the right kind of armor-piercing weapons.", 10, 60, 30, 50, 1, "Fleet Cruiser" },
                    { "Z-85E47CBD", 11, 5, "Cruiser", 5000000, 200, 20, "Almost every space-faring world has at least one space station in orbit. A station has no Speed score, no spike drive, and cannot perform any maneuvers in combat, though its transit jets can slowly move it around a solar system over a matter of weeks. Civilian trade stations allow for docking by bulk freighters and ships not cleared to land on the surface, while military stations strictly forbid any civilian docking.", 10, 120, 40, 50, null, "Small Station" },
                    { "Z-87D3F90A", 16, 5, "Fighter", 200000, 1, 1, "Small craft, often modified to replace the spike drive with a system drive. Their speed, cheapness, and combat utility make them a popular choice as inexpensive system patrol craft.", 1, 8, 2, 5, 5, "Strike Fighter" },
                    { "Z-8AFAE531", 11, 0, "Fighter", 200000, 10, 1, "The smallest craft that’s regularly used for interstellar drills, a shuttle is a cheap means of moving small amounts of precious material or important persons between worlds.", 1, 15, 5, 3, 3, "Shuttle" },
                    { "Z-EE0ED066", 11, 0, "Cruiser", 5000000, 40, 10, "This class of huge cargo ship is found most often in peaceful, heavily-populated sectors.", 2, 40, 25, 15, 0, "Bulk Freighter" }
                });

            migrationBuilder.InsertData(
                table: "ShipWeapon",
                columns: new[] { "Id", "AmmoCost", "Class", "Cost", "Description", "Dmg", "Hardpoints", "Mass", "Power", "Qualities", "TechLevel", "Type" },
                values: new object[,]
                {
                    { "Z-1398029C", null, "Fighter", 100000, "Stepped tapping of the spike drive power plant allows for the emission of a torrent of charged particles. The particles have very little armor penetration, but can fry a small ship’s power grid in a strike or two.", "3d4", 1, 1, 4, "Clumsy", 4, "Reaper battery" },
                    { "Z-15FD2A79", 5000, "Frigate", 50000, "Useless in ship-to-ship combat or against any other TL4 planet with working nuke snuffers, one of these missiles can still erase an entire lostworlder city without such protection.", "Special", 2, 1, 5, "Ammo 5", 4, "Nuclear Missiles" },
                    { "Z-1BE37D6E", null, "Fighter", 100000, "Twinned assay and penetration lasers modulate the frequency of this beam for remarkable armor penetration. These weapons are popular choices for fighters intended for frigate or cruiser engagement.", "1d4", 1, 1, 5, "AP 20", 4, "Multifocal Laser" },
                    { "Z-2BC6338C", null, "Cruiser", 2500000, "The SIP uses the ship’s spike phasing as an offensive weapon, penetrating the target with a brief incursion of MES energies that largely ignore attempts to evade.", "3d8", 3, 3, 10, "AP 15", 4, "Spike Inversion Projector" },
                    { "Z-31DF0627", null, "Cruiser", 1500000, "One of the first spinal-mount class weapons, the SBC briefly channels the full power of the ship into a charged beam. It lacks the power and penetration of the more advanced gravcannon, but also takes less power to mount.", "3d10", 3, 5, 10, "AP 15, Clumsy", 4, "Spinal Beam Cannon" },
                    { "Z-42C47194", null, "Fighter", 2000000, "A rare example of pretech weaponry, a fighter equipped with a PMB can scratch even a battleship’s hull.", "2d4", 1, 1, 5, "AP 25", 5, "Polyspectral MES Beam" },
                    { "Z-54A22797", 50000, "Capital", 5000000, "By firing projectiles almost as large as fighter-scale craft, the mass cannon inflicts tremendous damage on a target. Serious ammunition limitations hamper its wider-scale use.", "2d20", 4, 5, 10, "AP 20, Ammo 4", 4, "Mass Cannon" },
                    { "Z-5B7DB159", null, "Capital", 5000000, "A capital-class model of the SIP, a VTI is capable of incapacitating a cruiser in two hits. Its bulk limits its utility against fighter-class craft, however.", "3d20", 4, 10, 20, "AP 20, Clumsy", 4, "Vortex Tunnel Inductor" },
                    { "Z-5FD9FE02", null, "Frigate", 800000, "A focalized upgrade to the reaper battery, the CPC has a much better armor penetration profile.", "3d6", 2, 1, 10, "AP 16, Clumsy", 4, "Charged Particle Caster" },
                    { "Z-6425AFE7", 5000, "Frigate", 1000000, "A storm of magnetically-accelerated spike charges is almost guaranteed to eradicate any fighter-class craft it hits.", "2d6+2", 2, 2, 5, "Flak, AP 10, Ammo 5", 4, "Mag Spike Array" },
                    { "Z-70A39D9E", null, "Capital", 20000000, "One of the few surviving pretech weapons in anything resembling wide currency, this capital-class weapons system fires something mathematically related to a miniaturized black hole at a target.", "5d20", 5, 10, 25, "AP 25", 5, "Singularity Gun" },
                    { "Z-87B9E2D1", null, "Fighter", 50000, "Projecting a spray of tiny, dense particulate matter, sandthrowers are highly effective against lightly-armored fighters.", "2d4", 1, 1, 3, "Flak", 4, "Sandthrower" },
                    { "Z-911A4D52", null, "Cruiser", 2000000, "A swarm of self-directed microdrones sweeps over the ship. Their integral beam weaponry is too small to damage larger ships, but they can wipe out an attacking fighter wave.", "3d10", 2, 5, 10, "Cloud, Clumsy", 4, "Smart Cloud" },
                    { "Z-A8BDA2D4", null, "Capital", 4000000, "Modulation of the ship’s power core emits a cloak of MES lightning. While larger spike drive craft can shunt the energies away harmlessly, fighter-class ships are almost invariably destroyed if hit.", "1d20", 2, 5, 15, "AP 5, Cloud", 4, "Lightning Charge Mantle" },
                    { "Z-ABA7CCD7", 500, "Fighter", 200000, "A spray of penetrator sabots that use fractal surfacing to increase impact. Favored for bomber-class fighter hulls.", "2d6", 1, 1, 5, "AP 15, Ammo 4", 4, "Fractal Impact Charge" },
                    { "Z-ACF27F22", null, "Frigate", 700000, "With superior targeting and a smaller energy drain than a CPC, a plasma beam sacrifices some armor penetration.", "3d6", 2, 2, 5, "AP 10", 4, "Plasma Beam" },
                    { "Z-BF64855A", null, "Cruiser", 2000000, "Using much the same principles as man-portable grav weaponry, the gravcannon causes targets to fall apart in a welter of mutually-antagonistic gravitic fields.", "4d6", 3, 4, 15, "AP 20", 4, "Gravcannon" },
                    { "Z-F2361CAE", null, "Frigate", 500000, "A baseline frigate anti-fighter system, this battery fires waves of lasers or charged particles to knock down small craft.", "2d6", 1, 3, 5, "AP 10, Flak", 4, "Flak Emitter Battery" },
                    { "Z-F4830FAB", 2500, "Frigate", 500000, "Capable of damaging even a battleship, torpedoes are cumbersome, expensive, and often the core of a line frigate’s armament", "3d8", 1, 3, 10, "AP 20, Ammo 4", 4, "Torpedo Launcher" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipHull_ShipHullId",
                table: "ShipHull",
                column: "ShipHullId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipHull_ShipId",
                table: "ShipHull",
                column: "ShipId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipHull_ShipHullObject_ShipHullId",
                table: "ShipHull",
                column: "ShipHullId",
                principalTable: "ShipHullObject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipHull_Ships_ShipId",
                table: "ShipHull",
                column: "ShipId",
                principalTable: "Ships",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipHull_ShipHullObject_ShipHullId",
                table: "ShipHull");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipHull_Ships_ShipId",
                table: "ShipHull");

            migrationBuilder.DropIndex(
                name: "IX_ShipHull_ShipHullId",
                table: "ShipHull");

            migrationBuilder.DropIndex(
                name: "IX_ShipHull_ShipId",
                table: "ShipHull");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-009893FC");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-0E7E6633");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-0E846AC2");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-39BAAA8F");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-5D23908A");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-86BE2F61");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-8996B648");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-E85B9E7C");

            migrationBuilder.DeleteData(
                table: "ShipDefense",
                keyColumn: "Id",
                keyValue: "Z-EFF138B8");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-0151458F");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-04AFB664");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-06D61104");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-0D3828AD");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-0D795AD2");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-10F1CD90");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-135B5D6B");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-291BAC19");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-2B0D9E5A");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-2E44F3AA");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-320B836F");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-32F47F17");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-3FAB2AE9");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-4E8FAC56");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-63400B4F");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-6B550534");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-6D1D6B26");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-72D47DFB");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-73E39A84");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-74116D4C");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-74B0450B");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-78D28CBC");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-79A5800D");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-7C0C4782");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-8A596149");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-8B43469A");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-8F3BCE5C");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-97104540");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-9A824AD0");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-A41E6F4A");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-A932538E");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-AD80BB5A");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-AE9D3250");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-AEB24BDC");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-B27DC9D9");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-BC0C7702");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-C3DA94C7");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-C8009882");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-D0AF1677");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-D4B40E85");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-DA327974");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-E4C59176");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-E7ACDC8D");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-EBCB60A3");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-F4E71A51");

            migrationBuilder.DeleteData(
                table: "ShipFittingObject",
                keyColumn: "Id",
                keyValue: "Z-F95107AB");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-07CFCF2D");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-19405010");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-346F4C9C");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-3BD955C8");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-44409F37");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-449840F0");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-7D814EEA");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-831E2CDD");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-85E47CBD");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-87D3F90A");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-8AFAE531");

            migrationBuilder.DeleteData(
                table: "ShipHullObject",
                keyColumn: "Id",
                keyValue: "Z-EE0ED066");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-1398029C");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-15FD2A79");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-1BE37D6E");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-2BC6338C");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-31DF0627");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-42C47194");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-54A22797");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-5B7DB159");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-5FD9FE02");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-6425AFE7");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-70A39D9E");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-87B9E2D1");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-911A4D52");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-A8BDA2D4");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-ABA7CCD7");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-ACF27F22");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-BF64855A");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-F2361CAE");

            migrationBuilder.DeleteData(
                table: "ShipWeapon",
                keyColumn: "Id",
                keyValue: "Z-F4830FAB");

            migrationBuilder.AlterColumn<string>(
                name: "HullId",
                table: "Ships",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "ShipDefense",
                columns: new[] { "Id", "Class", "Cost", "CostExtra", "Description", "Effect", "Mass", "MassExtra", "Power" },
                values: new object[,]
                {
                    { "Z-017CD85F", "Frigate", 10000, true, "Too small to damage ships, these point defense lasers can detonate or melt incoming munitions, improving the ship’s defenses against torpedoes, fractal impact charges, and other ammunition-based weapons.", "+2 AC versus weapons that use ammo", 2, true, 3 },
                    { "Z-07844F08", "Fighter", 25000, true, "A complex glazing process can harden the surface of a ship’s armor to more effectively shed incoming attacks, decreasing the armor-piercing quality of any hit by 5.", "AP quality of attacking weapons reduced by 5", 1, true, 0 },
                    { "Z-0D67C59F", "Frigate", 25000, true, "The ship has hardened bulkheads, reinforced hatches, and specially-designed automated kill corridors for wiping out intruders. Provided that the ship’s bridge is still under control, the operator can prevent entry to the ship by any force that lacks shipyard-grade tools, weapons capable of melting hull plating, or specialized military breaching implements. If intruders do get inside, only well-equipped, specially-trained marines have any real chance of breaching the defenses. Ordinary space pirates or more casual invaders have only a 1 in 6 chance of threatening the bridge crew, though they may cause significant damage in their dying.", "Makes enemy boarding more difficult", 1, true, 2 },
                    { "Z-3FB87475", "Frigate", 50000, true, "This system links with a ship’s navigational subsystem and randomizes the motion vectors in sympathy with metadimensional gravitic currents. This agility gives any hit on the ship a 1 in 6 chance of being negated entirely.", "1 in 6 change of any given attack missing", 2, true, 5 },
                    { "Z-404CFB0D", "Frigate", 25000, true, "A high-powered secondary ECM generator can be activated to negate any one otherwise-successful hit against the ship. This generator can be activated after the damage has been rolled, but enemy ships rapidly compensate for the new ECM source, and so the generator can only be used effectively once per engagement", "Negate one successful hit", 1, true, 2 },
                    { "Z-45B760DD", "Capital", 100000, true, "By sacrificing empty hull space in a complex system of ablative blast baffles, a capital-class ship can have a large amount of its total mass shot away without actually impinging on its normal function. This grants it a +1 AC bonus and 20 extra maximum hit points.", "+1 AC, +20 maximum hit points", 2, true, 5 },
                    { "Z-A203AA4C", "Cruiser", 10000, true, "These drones are invariably short-lived due to the enormous energy signatures they produce, but until the ship’s next turn they grant a +2 AC bonus as their emissions confuse foes. Foxer drones are cheaply constructed and essentially free; the only limit on their number is the amount of free space set aside for holding them.", "+2 AC for one round when fired, ammo 5", 1, true, 2 },
                    { "Z-B567FE10", "Frigate", 50000, true, "The ship is equipped with an array of gravitic braker guns and an upgraded nuke snuffer field. While useless in conventional ship-to-ship combat, the array can deflect or dampen meteor impacts, dropped penetrator rods, or other non-powered bombardment techniques, and the snuffer field is powerful enough to prevent nuclear fission reactions over a hemisphere-sized area. A single ship with a PDA can protect against any natural meteor strikes and deny easy terror bombardment of a planet’s population. The PDA cannot fully protect against orbital strikes by powered penetrators, but it can nudge them off course and make pinpoint strikes impractical. Most developed planets have much more powerful and effective ground installations, but a PDA-equipped ship is a useful emergency stopgap for poor or primitive worlds.", "Anti-impact and anti-nuke surface defenses", 2, true, 4 },
                    { "Z-C1411811", "Fighter", 25000, true, "At the cost of a certain amount of speed and maneuverability, a ship can have its armor plating reinforced against glancing hits, gaining a +2 bonus to its AC. This augmentation can decrease a ship’s Speed below 0, meaning it will be applied as a penalty to all Pilot tests.", "+2 AC, -1 speed", 1, true, 0 }
                });

            migrationBuilder.InsertData(
                table: "ShipFittingObject",
                columns: new[] { "Id", "Class", "Cost", "CostExtra", "Description", "Effect", "Mass", "MassExtra", "Power", "PowerExtra", "Type" },
                values: new object[,]
                {
                    { "Z-0567568C", "Frigate", 10000, true, " Each time this fitting is selected, 10% of the ship’s maximum crew gain access to luxury cabins of a spaciousness sufficient to please a wealthy star-farer. This fitting comes with the usual zero-gee athletic courts, decorative fountains, fine dining, and artistic fittings.", "10% of the max crew get luxurious quarters", 1.0, true, 1, false, "Luxury cabins" },
                    { "Z-0C4010FF", "Frigate", 2500, true, "Selecting this fitting equips the ship with a number of single-use escape craft capable of reaching the nearest habitable planet or station in a star system. If no such destination exists, the boats can maintain their passengers for up to a year in drugged semi-stasis. Lifeboats have fully-functional comm systems and are usually equipped with basic survival supplies and distress beacons. A single selection of this fitting provides enough lifeboats for a ship’s maximum crew, with up to twenty people per boat.", "Emergency escape craft for a ship’s crew", 1.0, false, 0, false, "Lifeboats" },
                    { "Z-123FAADB", "Fighter", null, false, "The ship’s standard drive-1 spike drive is removed and replaced with a different propulsion system. The ship is still treated as having a drive-1 for maneuvering and system transit purposes, but it cannot make interstellar drills. This modification lowers the cost of the basic hull by 10% and adds extra power and space based on the size of the hull: 1 power for fighters, 2 for frigates, 3 for cruisers, and 4 for capital ship hulls. Twice this amount of free mass is gained by the process.", "Replace spike drive with small system drive", -2.0, true, -1, true, "System drive" },
                    { "Z-1A13F862", "Fighter", 10000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-2 rating", 1.0, true, 1, true, "Drive-2 upgrade" },
                    { "Z-1AA91E37", "Frigate", 5000, true, "All ships are equipped with basic medical facilities for curing lightly injured crew members and keeping the seriously injured ones stable until reaching a planet. An extended medbay improves those facilities, allowing for the medical treatment of up to the ship’s entire maximum crew at once, including the treatment of critically wounded passengers.", "Can provide medical care to more patients", 1.0, false, 1, false, "Extended medbay" },
                    { "Z-1D46B008", "Cruiser", 50000, true, "The ship is equipped with a full-scale TL4 fabrication plant programmed to support its needs. The ship can stock raw materials and parts at 5,000 credits per ton; these parts can then be used to “pay” ship repair or maintenance costs when conventional shipyards are unavailable. The factory can also create and repair vehicles, TL4 equipment, space habs, and planetary structures with these parts at a rate of 10,000 credits worth of construction a day. If a mobile extractor is available, the raw materials processed by the latter unit can be used to feed the factory. Operating a mobile factory requires at least 100 well-trained personnel. Each 10 fewer available doubles repair or maintenance times.", "Self-sustaining factory and repair facilities", 2.0, true, 3, false, "Mobile factory" },
                    { "Z-1ED7CA25", "Fighter", 5000, true, "The ship can be designed to accommodate a larger number of crew or passengers. Extended life support can be fitted multiple times; each time, the maximum crew rating of the ship increases by 100% of its normal maximum. Thus, a free merchant who installs this twice can have a maximum complement of 18 people.", "Doubles maximum crew size", 1.0, true, 1, true, "Extended life support" },
                    { "Z-285CD043", "Cruiser", 50000, true, "These banked rows of compressed cold sleep pods are designed to carry enormous numbers of people in extended hibernation, most often colonists to some new homeworld or escapees from some stellar disaster. A cruiser can carry up to 1,000 colonists in stasis, while a capital ship can handle up to 5,000. Each further time this fitting is selected, these numbers double. These pods put their inhabitants into very deep hibernation so as to minimize the resources necessary to maintain their lives. Bringing them out of this stasis requires a month of “defrost”. Crash awakenings have a 25% chance of killing the subject. The pods are rated for 100 years of stasis, but their actual maximum duration is somewhat speculative.", "House vast numbers of cold sleep passengers", 2.0, true, 1, true, "Exodus bay" },
                    { "Z-2A772E8D", "Fighter", 25000, true, "This fitting includes the benefits of the Atmospheric Configuration fitting, as well as allowing the ship to operate while immersed in a liquid medium. Submerged ships cannot be detected with conventional planetary traffic sensors and require military sonar and naval sensors to fix their position, resources often unavailable on less developed worlds. So long as the ship stays away from military naval craft and bases, it is almost impossible to track while submerged. Only fighter and frigate hull classes can mount this fitting.", "Can land and can operate under water", 1.0, true, 1, false, "Amphibious operation" },
                    { "Z-32E1DF63", "Frigate", 2000, true, "Much like an armory, this option allows a captain to lay in a general supply of equipment likely to be useful to explorers and spacemen. Any TL4 equipment on the gear list can be found in the ship’s locker in amounts commensurate with the ship’s size. A few guns and some basic armor might be included as well, but for serious armament an armory is required. There is enough gear available to outfit the entire crew for normal use, but giving it away or losing it in use may deplete the locker until it is restocked.", "General equipment for the crew", 0.0, false, 0, false, "Ship’s locker" },
                    { "Z-39EB539C", "Frigate", 50000, false, "Automated mining and refinery equipment has been built into the ship, allowing it to extract resources from asteroids and planetary surfaces. Careful extraction of specific lodes of valuable minerals can be quite profitable at the GM’s discretion, but if the crew is simply melting down available asteroids for raw materials, the unit can refine one ton of usable materials per day worth about 500 credits in most markets. These raw materials can be used to feed a mobile factory, and a ship may have more than one mobile extractor fitted to it to multiply the return if sufficient raw feedstock is available. Operating an extractor requires at least five crew members.", "Space mining and refinery fittings", 1.0, false, 2, false, "Mobile extractor" },
                    { "Z-3D0864B3", "Frigate", 5000, true, "Most ships require only basic analysis of a star system, sufficient to identify population centers, do rough scanning of an object’s composition, and chart major navigational hazards. Survey sensor arrays greatly enhance the ship’s sensor abilities, allowing for finely-detailed mapping of objects and planets, along with broad-spectrum communications analysis. They also improve attempts to detect other craft when scanning a region for stealthy vessels. Any rolls with survey sensor arrays add +2 to skill checks.", "Improved planetary sensory array", 1.0, false, 2, false, "Survey sensor array" },
                    { "Z-3EB7A598", "Fighter", 2500, true, "A normal complement of ship’s stores can keep the maximum crew size supplied for two months. Each selection of Extended Stores doubles that time, and can be fitted multiple times.", "Maximum life support duration is doubled", 1.0, true, 0, false, "Extended stores" },
                    { "Z-47519D08", "Frigate", 300000, false, "Armored and stealthed versions of cargo lighters, these craft are twice as fast, apply a -3 penalty to tracking and targeting skill checks, and can carry up to one hundred troops or passengers. Many are equipped with assorted Heavy weapons to clear the landing zone, and can be treated as flight-capable gravtanks for purposes of combat. This fitting can be purchased multiple times.", "Stealthed landing pod for troops", 2.0, false, 0, false, "Drop pod" },
                    { "Z-543A3B18", "Frigate", 10000, true, "Rather than maintaining lengthy lists of ship equipment, a captain can simply buy an armory. Ships so equipped have whatever amounts of TL4 military-grade weaponry and armor that a crew might require, and integral maintenance facilities for its upkeep. There is enough gear available to outfit the entire crew for normal use, but giving it away or losing it in use may deplete it", "Weapons and armor for the crew", 0.0, false, 0, false, "Armory" },
                    { "Z-5A52AA98", "Frigate", null, false, "This pretech relic is unavailable on the open market in most sectors. Once installed in a ship, it can be “imprinted” by up to a dozen psychics. These psychics can sense and affect any object or location within ten meters of the relic after one minute of concentration, provided they are within the same solar system. Thus, teleporters can always teleport next to the anchorpoint, telepaths can always contact anyone within the affected zone, telekinetics can manipulate objects near the relic, and so forth. The relic can be recoded to eliminate existing imprints, but any psychic who gets access to the relic for ten minutes of meditation and focus can imprint on it afterwards. There is no obvious way to tell how many psychics have imprinted to the anchorpoint.", "Focal point for allied psychics’ powers", 0.0, false, 3, false, "Psionic anchorpoint" },
                    { "Z-620A2D04", "Fighter", 2500, true, "Most ships require refueling after each drill jump, no matter the distance. Installing fuel bunkers allows the ship to carry one additional load of fuel. This fitting can be installed multiple times for ships that wish to minimize fueling.", "Adds fuel for one more drill between fuelings", 1.0, false, 0, false, "Fuel bunkers" },
                    { "Z-69F82B04", "Fighter", 5000, true, "This fitting must be put in place when the ship is built, and cannot be installed on cruiser-class or larger ships. A ship designed for atmospheric flight can land on most solid or aqueous surfaces.", "Can land: frigates and fighters only.", 1.0, true, 0, false, "Atmospheric configuration" },
                    { "Z-6BDF2B43", "Fighter", 20000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-3 rating", 2.0, true, 2, true, "Drive-3 upgrade" },
                    { "Z-6D56D5E0", "Frigate", 25000, true, "Drills along a known spike drive route have no chance of failure so long as the route is no longer than twice the operating pilot’s skill level and does not involve course trimming. Thus, a navigator with Pilot-1 skill would always succeed in making a drill of two hexes or less, provided the route was a known one. The drill course regulator requires a sophisticated technical base and compatible metadimensional energy conditions in a given sector; many sectors no longer have this technology or lack the right environment to use it. If the GM prefers a campaign where space travel is always at least somewhat dangerous, they may deny access to this fitting. Ships piloted by exceptionally talented navigators may choose not to mount it even then.", "Common drill routes become auto-successes", 1.0, false, 1, true, "Drill course regulator" },
                    { "Z-7412C698", "Frigate", null, false, "This TL5 tech is completely unavailable in most sectors and was uncommon even on Mandate-era ships. The pads allow up to a dozen people or 1,200 kilograms of matter to be teleported to and from the surface of a planet or the interior of a another ship, provided it is no more than a few tens of thousands of kilometers distant. Ship-to-ship teleportation is possible only when the receiving ship is cooperating by transmitting accurate coordinate details; otherwise, a friendly, unjammed signal from inside is necessary to lock onto the target point. Planetary teleportations are possible only in the absence of physical barriers between the ship and the target point below. The teleporters may be used once every five minutes. In those rare sectors where this tech is widely available, it costs 200k.", "Pretech teleportation to and from ship", 1.0, false, 1, false, "Teleportation pads" },
                    { "Z-79679EB6", "Fighter", 25000, true, "Stealth systems can mask the ship’s energy emissions through careful modulation of the output. All travel times inside a star system are doubled when the system is engaged, but any skill checks to avoid detection gain a +2 bonus.", "Adds +2 to skill checks to avoid detection", 1.0, true, 1, true, "Emissions dampers" },
                    { "Z-7FDF1B28", "Frigate", 10000, true, "Gravitic projectors allow the ship to manipulate objects within its immediate area, pushing, pulling, and sliding objects no larger than a ship of one hull class smaller. Targets with shipscale propulsion can resist the beams, but lifeboats, individual suit jets, or other smaller propulsors are inadequate. The beam can focus on only one object at a time but can move it into the ship’s cargo bays or hurl it out of the ship’s immediate vicinity within three rounds. The beams can only effectively manipulate objects in very low gravity, and not those on the surface of planets or other objects with significant natural gravity.", "Manipulate objects in space at a distance", 1.0, false, 2, false, "Tractor beams" },
                    { "Z-80B3847E", "Frigate", 100000, true, "An extremely rare example of psitech dating from before the Scream, a precognitive nav chamber allows a character with at least Precognition-2 psionic skill to assist in interstellar drills, sensing impending shear alterations before they happen. The navigator automatically succeeds on any spike drill check of difficulty 9 or less. On a failed check, add 2 to the Spike Drive Mishap roll, limiting the potential damage. On drilling in to the destination system, the psychic has expended all Effort in the process.", "Allows a precog to assist in navigation", 0.0, false, 1, false, "Precognitive nav chamber" },
                    { "Z-87E19910", "Frigate", 10000, true, "The ship can disguise its long-range sensor readings, spoofing scans with the ID tags and apparent hull type of any other ship of its choice. To penetrate this masquerade, the scanning entity must beat a Wis/Program skill check against difficulty 10 plus the Program skill of the masking ship’s comms officer. Once the ship is close enough to visually identify, the masking is useless.", "At long distances, disguise ship as another", 0.0, false, 1, true, "Sensor mask" },
                    { "Z-933CFAB1", "Frigate", 2500, true, "The ship has been fitted with specialized mounts, bunking, cargo bays, and other facilities to expedite the transport of often-unwieldy vehicles, including mechs and other military craft. Any vehicles carried count as only half their usual cargo tonnage. Assuming trained operators, up to four vehicles can be offloaded or ramped onto the ship per round in cases where speed is critical.", "Halve tonnage space of carried vehicles", 1.0, true, 0, false, "Vehicle transport fittings" },
                    { "Z-9BC5C03A", "Fighter", 2500, true, " Carefully-designed storage space intended to conceal illicit cargo from customs inspection. Each installation of this fitting adds 200 kilograms of cargo space in a fighter, 2 tons in a frigate, 20 tons in a cruiser, or 200 tons in a capital ship. Cargo in a smuggler’s hold will never be found by a standard customs inspection. Careful investigation by a suspicious official can find it on a difficulty 10 check using their Wis/Notice skill, and a week-long search one step short of disassembly will find it on a difficulty 7 check.", "Small amount of well-hidden cargo space", 1.0, false, 0, false, "Smuggler’s hold" },
                    { "Z-9D6FA079", "Frigate", 25000, false, "Cruisers and larger craft can’t land on planetary bodies, so they require small shuttlecraft for transport. A cargo lighter is only capable of surface-to-orbit flight, which takes roughly twenty minutes either way, but can latch on to a standard pressurized cargo container holding up to 200 tons of cargo and passengers. These containers are usually collapsible and take up no significant space when compressed for storage, assuming they’re not simply disposable cargo shells. This fitting can be purchased multiple times.", "Orbit-to-surface cargo shuttle", 2.0, false, 0, false, "Cargo lighter" },
                    { "Z-9E34B27A", "Frigate", 10000, true, "Forging new spike courses is too much an art to rely on computerized assistance, but an advanced nav computer can help on well-mapped routes. When navigating an interstellar drill course with charts less than a year old, the navigator decreases drill difficulty by 2.", "Adds +2 for traveling familiar spike courses", 0.0, false, 1, true, "Advanced nav computer" },
                    { "Z-A2538CF5", "Frigate", 500, true, "On-board workshops can be bought at smaller than maximum sizes than a hull would allow, if additional TL4 tech facilities aren’t strictly needed. A frigate-sized workshop is sufficient for modding any personal gear as per the modification rules on page 100. A cruiser-sized workshop can handle vehicle modding and starship maintenance, and a capital-class workshop can build vehicles or similar large work from scratch at full cost.", "Automated tech workshops for maintenance", 0.5, true, 1, false, "Workshop" },
                    { "Z-A6905540", "Fighter", 50000, false, "Some ships run with more guns than crewmen. A single NPC or PC can man one gun per round, firing it as often as the gunnery chief’s Fire One Weapon or Fire All Guns actions allow, but sometimes that’s not enough. Installing an autonomic targeting system for a gun allows it to shoot at a +2 hit bonus without human assistance. This system must be installed once for each gun that is to be self-manned.", "Fires one weapon system without a gunner", 0.0, false, 1, false, "Auto-targeting system" },
                    { "Z-A7D06CD7", "Cruiser", 10000, true, "Some ships are designed to produce food and air supplies for the crew. Selecting hydroponic production allows for the indefinite supply of a number of crewmen equal to the ship’s maximum crew. This option may be taken multiple times for farm ships, in which case each additional selection doubles the number of people the ship can support.", "Ship produces life support resources", 2.0, true, 1, true, "Hydroponic production" },
                    { "Z-AB2D2C47", "Fighter", 10000, true, "The ship has been carefully fitted to support the use of non-sentient expert system robots in its operation. At least one human, VI, or True AI crew member is necessary to oversee the bots and monitor spike drills, but otherwise crew may be replaced with cheap, basic robots at a cost of 1,000 credits per crew member replaced. Bots don’t draw pay, don’t take up life support, and their maintenance is assumed to be part of the ship’s operating costs. These bots are incapable of any actions unrelated to operating the ship and are treated as level-0 in their skills where relevant.", "Ship can use simple robots as crew", 1.0, false, 2, false, "Automation support" },
                    { "Z-B306D6E6", "Frigate", 10000, true, "A lab suitable for investigating alien xenolife, planetary geology, esoteric technology, and other mysteries of the cosmos. The lab contains cold sleep pods adequate to contain alien samples, viro-shielded research cells, high-energy lasers, and other requisite tools. When used to investigate some phenomenon or object, any applicable skill rolls are improved by +1 for a frigate lab, +2 for a cruiser lab, and +3 for a capital ship lab.", "Skill bonus for analysis and research", 2.0, false, 1, true, "Advanced lab" },
                    { "Z-B62E469C", "Frigate", 100000, true, "The ship has been designed to act as the core of a future settlement. Once this fitting is engaged, the ship ceases to be operational as a starship, and builds out into a set of habitats, hydroponic gardens, fusion plants, and living spaces sufficient to support up to five times its maximum crew, including enough fabrication and workshop facilities to keep the settlement operational under normal conditions. The settlement can be a deep-space hab, orbital installation, or planetary settlement. In the latter case, the ship must land to form the settlement; even ships without atmospheric operations can do so, but they can never take off again. Once activated, a colony core cannot be re-packed.", "Ship can be deconstructed into a colony base", 2.0, true, 4, false, "Colony core" },
                    { "Z-C3AF7E43", "Capital", 1000000, false, "These sophisticated docking bays provide all the necessary tools and support for launching a starship from the mother craft. They are rarely seen outside of dedicated capital-class carriers, but some cruisers make room to mount a fighter-class attack shuttle. Each bay allows room for one ship of the appropriate hull class. While the carried ship can support its own crew if necessary, most carriers fold their space wing into the mothership’s crew roster. This fitting can be taken multiple times.", "Carrier housing for a frigate", 4.0, false, 1, false, "Ship bay/frigate" },
                    { "Z-C5C60697", "Frigate", 5000, true, " Armored tubes equipped with laser cutter apertures can be used to forcibly invade a hostile ship, provided the target’s engines have been disabled. Ships without boarding tubes have to send invaders across empty space to either make an assault on a doubtless heavily-guarded airlock or cut their way in through the hull with laser cutters and half an hour of work.", "Allows boarding of a hostile disabled ship", 1.0, false, 0, false, "Boarding tubes" },
                    { "Z-C74686AD", "Cruiser", 200000, false, "These sophisticated docking bays provide all the necessary tools and support for launching a starship from the mother craft. They are rarely seen outside of dedicated capital-class carriers, but some cruisers make room to mount a fighter-class attack shuttle. Each bay allows room for one ship of the appropriate hull class. While the carried ship can support its own crew if necessary, most carriers fold their space wing into the mothership’s crew roster. This fitting can be taken multiple times.", "Carrier housing for a fighter", 2.0, false, 0, false, "Ship bay/fighter" },
                    { "Z-C79799B4", "Cruiser", 500000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-6 rating", 4.0, true, 3, true, "Drive-6 upgrade" },
                    { "Z-C81466A5", "Frigate", 40000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-4 rating", 3.0, true, 2, true, "Drive-4 upgrade" },
                    { "Z-CB58A963", "Fighter", 0, false, "Free mass can be traded for pressurized cargo space. Tracked by weight for convenience, one cubic meter is usually one ton, with most vehicles requiring ten tons when loaded, tanks taking 25, and aircraft or mechs taking up 50 tons of cargo space. One point of free mass grants 2 tons of cargo space in a fighter, 20 tons in a frigate, 200 tons in a cruiser, and 2000 tons in a capital-class ship. This fitting can be purchased multiple times.", "Pressurized cargo space", 1.0, false, 0, false, "Cargo space" },
                    { "Z-D24E35D9", "Frigate", 25000, true, "The ship is designed with symbiosis mounts that allow other ships to “hitch” on the craft’s spike drills. Each shiptender mount allows one craft of a hull size smaller than the tender to link up for intersystem drills. Mounts cannot be used for in-system travel. If the linking ship has been designed to be carried by a tender then establishing this link takes one hour. If not, it takes a full day to fit the ship into the mount. Ships can dismount from the tender with an hour’s disentanglement. In an emergency, the carried ships can dismount instantly, but the mountings are considered disabled then until repairs are made. Carried ships cannot fight.", "Allow another ship to hitch on a spike drive", 1.0, false, 1, false, "Shiptender mount" },
                    { "Z-DB3A5684", "Fighter", 0, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "The standard spike drive common among all ships", 0.0, true, 0, true, "Drive-1" },
                    { "Z-E494F028", "Frigate", 5000, true, "Fuel scoops allow for the harvesting and extraction of hydrogen from gas giants or the penumbra of solar bodies. The extraction process requires four days of processing and refinement, but completely refuels the ship. Such fittings are common on explorer craft that cannot expect to find refueling stations.", "Ship can scoop fuel from a gas giant or star", 1.0, true, 2, false, "Fuel scoops" },
                    { "Z-EA14873B", "Frigate", 5000, true, "These stasis pods can keep a subject alive for centuries provided that the ship’s power doesn’t fail. Each installation allows for keeping a number of people equal to the ship’s maximum crew in stasis indefinitely. This fitting can be installed multiple times.", "Keeps occupants in stasis", 1.0, false, 1, false, "Cold sleep pods" },
                    { "Z-F9381343", "Frigate", 100000, true, "A ship can improve its standard-issue drive-1 spike drive with additional phase filters and power throughput refinements. A captain needs buy only the final grade of drive desired. He does not have to buy upgrades sequentially. Drives of rating 4 and higher are generally TL5 artifacts that cannot be built most modern worlds.", "Upgrade a spike drive to drive-5 rating", 3.0, true, 3, true, "Drive-5 upgrade" }
                });

            migrationBuilder.InsertData(
                table: "ShipHullObject",
                columns: new[] { "Id", "Ac", "Armor", "Class", "Cost", "CrewMax", "CrewMin", "Description", "Hardpoints", "Hp", "Mass", "Power", "Speed", "Type" },
                values: new object[,]
                {
                    { "Z-10BE462F", 16, 20, "Capital", 50000000, 1000, 200, " Dreaded hulks of interstellar war, very few worlds have the necessary technology or economy to support the massive expense of building and crewing a battleship. Those that do gain access to a ship that is largely invulnerable to anything short of specially-designed anti-capital cruisers or hunter-killer frigates.", 15, 100, 50, 75, 0, "Battleship" },
                    { "Z-2054C4DB", 14, 15, "Cruiser", 10000000, 200, 50, "The favored ship of the line of most wealthy, advanced worlds, and often the biggest and most powerful ship most planets can build. A cruiser’s heavy armor and infrastructural support for heavy guns make it a lethal weapon against frigates and any other ship not optimized for cracking heavy armor. They can prove vulnerable to swarm attacks by fighter-bombers equipped with the right kind of armor-piercing weapons.", 10, 60, 30, 50, 1, "Fleet Cruiser" },
                    { "Z-394B13F4", 14, 2, "Frigate", 500000, 6, 1, "A hull type much beloved by adventurers, a free merchant has unimpressive combat utility but can carry substantial amounts of cargo while mounting enough weaponry to discourage small-craft piracy.", 2, 20, 15, 10, 3, "Free Merchant" },
                    { "Z-48769B70", 14, 5, "Frigate", 2500000, 20, 5, "The hull of choice for customs cutters and system law enforcement, the patrol boat is a light frigate built heavy enough to overawe small merchant vessels while still being relatively cheap to build and crew.", 4, 25, 10, 15, 4, "Patrol Boat" },
                    { "Z-62DF9C6C", 14, 10, "Capital", 60000000, 1500, 300, "The queen of a fleet, even fewer polities can afford to build one of these huge ships. Carriers can support flights of fighter or frigate-class warships, ones specially equipped to handle particular missions. This versatility allows it to load fighter-bombers for anti-capital missions one month, and then switch to swarms of hunter-killer frigates the next when a hostile system’s asteroid outposts need to be destroyed. Stripped of its combat wings, however, a carrier has less individual firepower than a cruiser.", 4, 75, 100, 50, 0, "Carrier" },
                    { "Z-6F79B731", 13, 10, "Frigate", 4000000, 40, 10, "The smallest true combat frigate, and often simply called a “frigate” by spacers. Corvettes have significantly thicker armor than patrol boats and trade additional crew needs and less maneuverability for more available free mass.", 6, 40, 15, 15, 2, "Corvette" },
                    { "Z-89B40594", 11, 5, "Cruiser", 5000000, 200, 20, "Almost every space-faring world has at least one space station in orbit. A station has no Speed score, no spike drive, and cannot perform any maneuvers in combat, though its transit jets can slowly move it around a solar system over a matter of weeks. Civilian trade stations allow for docking by bulk freighters and ships not cleared to land on the surface, while military stations strictly forbid any civilian docking.", 10, 120, 40, 50, null, "Small Station" },
                    { "Z-8B63A634", 17, 20, "Capital", 40000000, 1000, 100, "Almost every space-faring world has at least one space station in orbit. A station has no Speed score, no spike drive, and cannot perform any maneuvers in combat, though its transit jets can slowly move it around a solar system over a matter of weeks. Civilian trade stations allow for docking by bulk freighters and ships not cleared to land on the surface, while military stations strictly forbid any civilian docking.", 30, 120, 75, 125, null, "Large Station" },
                    { "Z-B7A240C3", 15, 10, "Frigate", 7000000, 120, 30, "The heaviest starship that most poor or resource-deprived worlds can build, the heavy frigate can carry a significant loadout of weaponry and has enough crew to overwhelm most pirate ships if it comes to a boarding action. While it packs a substantial punch, it lacks the armor of a true cruiser-class warship.", 8, 50, 20, 25, 1, "Heavy Frigate" },
                    { "Z-BE931FB4", 11, 0, "Cruiser", 5000000, 40, 10, "This class of huge cargo ship is found most often in peaceful, heavily-populated sectors.", 2, 40, 25, 15, 0, "Bulk Freighter" },
                    { "Z-D13E4100", 16, 5, "Fighter", 200000, 1, 1, "Small craft, often modified to replace the spike drive with a system drive. Their speed, cheapness, and combat utility make them a popular choice as inexpensive system patrol craft.", 1, 8, 2, 5, 5, "Strike Fighter" },
                    { "Z-F3DB144D", 11, 0, "Fighter", 200000, 10, 1, "The smallest craft that’s regularly used for interstellar drills, a shuttle is a cheap means of moving small amounts of precious material or important persons between worlds.", 1, 15, 5, 3, 3, "Shuttle" }
                });

            migrationBuilder.InsertData(
                table: "ShipWeapon",
                columns: new[] { "Id", "AmmoCost", "Class", "Cost", "Description", "Dmg", "Hardpoints", "Mass", "Power", "Qualities", "TechLevel", "Type" },
                values: new object[,]
                {
                    { "Z-0A3FCC41", null, "Frigate", 500000, "A baseline frigate anti-fighter system, this battery fires waves of lasers or charged particles to knock down small craft.", "2d6", 1, 3, 5, "AP 10, Flak", 4, "Flak Emitter Battery" },
                    { "Z-108735D7", null, "Cruiser", 2000000, "A swarm of self-directed microdrones sweeps over the ship. Their integral beam weaponry is too small to damage larger ships, but they can wipe out an attacking fighter wave.", "3d10", 2, 5, 10, "Cloud, Clumsy", 4, "Smart Cloud" },
                    { "Z-1CE94599", null, "Capital", 20000000, "One of the few surviving pretech weapons in anything resembling wide currency, this capital-class weapons system fires something mathematically related to a miniaturized black hole at a target.", "5d20", 5, 10, 25, "AP 25", 5, "Singularity Gun" },
                    { "Z-1E5B1E13", null, "Cruiser", 1500000, "One of the first spinal-mount class weapons, the SBC briefly channels the full power of the ship into a charged beam. It lacks the power and penetration of the more advanced gravcannon, but also takes less power to mount.", "3d10", 3, 5, 10, "AP 15, Clumsy", 4, "Spinal Beam Cannon" },
                    { "Z-22324E8E", null, "Cruiser", 2500000, "The SIP uses the ship’s spike phasing as an offensive weapon, penetrating the target with a brief incursion of MES energies that largely ignore attempts to evade.", "3d8", 3, 3, 10, "AP 15", 4, "Spike Inversion Projector" },
                    { "Z-279F586A", null, "Fighter", 100000, "Twinned assay and penetration lasers modulate the frequency of this beam for remarkable armor penetration. These weapons are popular choices for fighters intended for frigate or cruiser engagement.", "1d4", 1, 1, 5, "AP 20", 4, "Multifocal Laser" },
                    { "Z-43D0FA6A", 5000, "Frigate", 50000, "Useless in ship-to-ship combat or against any other TL4 planet with working nuke snuffers, one of these missiles can still erase an entire lostworlder city without such protection.", "Special", 2, 1, 5, "Ammo 5", 4, "Nuclear Missiles" },
                    { "Z-4D39338F", null, "Frigate", 800000, "A focalized upgrade to the reaper battery, the CPC has a much better armor penetration profile.", "3d6", 2, 1, 10, "AP 16, Clumsy", 4, "Charged Particle Caster" },
                    { "Z-4E1DDD40", null, "Fighter", 100000, "Stepped tapping of the spike drive power plant allows for the emission of a torrent of charged particles. The particles have very little armor penetration, but can fry a small ship’s power grid in a strike or two.", "3d4", 1, 1, 4, "Clumsy", 4, "Reaper battery" },
                    { "Z-60A9D83F", 50000, "Capital", 5000000, "By firing projectiles almost as large as fighter-scale craft, the mass cannon inflicts tremendous damage on a target. Serious ammunition limitations hamper its wider-scale use.", "2d20", 4, 5, 10, "AP 20, Ammo 4", 4, "Mass Cannon" },
                    { "Z-81180EB2", 2500, "Frigate", 500000, "Capable of damaging even a battleship, torpedoes are cumbersome, expensive, and often the core of a line frigate’s armament", "3d8", 1, 3, 10, "AP 20, Ammo 4", 4, "Torpedo Launcher" },
                    { "Z-8B9D3AC0", null, "Capital", 5000000, "A capital-class model of the SIP, a VTI is capable of incapacitating a cruiser in two hits. Its bulk limits its utility against fighter-class craft, however.", "3d20", 4, 10, 20, "AP 20, Clumsy", 4, "Vortex Tunnel Inductor" },
                    { "Z-8D2C547D", null, "Capital", 4000000, "Modulation of the ship’s power core emits a cloak of MES lightning. While larger spike drive craft can shunt the energies away harmlessly, fighter-class ships are almost invariably destroyed if hit.", "1d20", 2, 5, 15, "AP 5, Cloud", 4, "Lightning Charge Mantle" },
                    { "Z-8FDCE1ED", null, "Frigate", 700000, "With superior targeting and a smaller energy drain than a CPC, a plasma beam sacrifices some armor penetration.", "3d6", 2, 2, 5, "AP 10", 4, "Plasma Beam" },
                    { "Z-9D523546", 500, "Fighter", 200000, "A spray of penetrator sabots that use fractal surfacing to increase impact. Favored for bomber-class fighter hulls.", "2d6", 1, 1, 5, "AP 15, Ammo 4", 4, "Fractal Impact Charge" },
                    { "Z-BAB6A48B", null, "Cruiser", 2000000, "Using much the same principles as man-portable grav weaponry, the gravcannon causes targets to fall apart in a welter of mutually-antagonistic gravitic fields.", "4d6", 3, 4, 15, "AP 20", 4, "Gravcannon" },
                    { "Z-D5A00BAD", null, "Fighter", 2000000, "A rare example of pretech weaponry, a fighter equipped with a PMB can scratch even a battleship’s hull.", "2d4", 1, 1, 5, "AP 25", 5, "Polyspectral MES Beam" },
                    { "Z-E9AB7A1B", 5000, "Frigate", 1000000, "A storm of magnetically-accelerated spike charges is almost guaranteed to eradicate any fighter-class craft it hits.", "2d6+2", 2, 2, 5, "Flak, AP 10, Ammo 5", 4, "Mag Spike Array" },
                    { "Z-F86B7EE9", null, "Fighter", 50000, "Projecting a spray of tiny, dense particulate matter, sandthrowers are highly effective against lightly-armored fighters.", "2d4", 1, 1, 3, "Flak", 4, "Sandthrower" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_HullId",
                table: "Ships",
                column: "HullId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_ShipHullObject_HullId",
                table: "Ships",
                column: "HullId",
                principalTable: "ShipHullObject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
