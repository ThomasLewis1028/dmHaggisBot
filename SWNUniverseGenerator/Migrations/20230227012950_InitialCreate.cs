using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWNUniverseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                });

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

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<string>(type: "TEXT", nullable: false),
                    CargoItemId = table.Column<string>(type: "TEXT", nullable: true),
                    ContactId = table.Column<string>(type: "TEXT", nullable: true),
                    DestId = table.Column<string>(type: "TEXT", nullable: true),
                    Pay = table.Column<int>(type: "INTEGER", nullable: false),
                    UniverseId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Jobs_Characters_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_Item_CargoItemId",
                        column: x => x.CargoItemId,
                        principalTable: "Item",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_Jobs_Planets_DestId",
                        column: x => x.DestId,
                        principalTable: "Planets",
                        principalColumn: "Id");
                });

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
                name: "IX_Jobs_CargoItemId",
                table: "Jobs",
                column: "CargoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ContactId",
                table: "Jobs",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DestId",
                table: "Jobs",
                column: "DestId");

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
                name: "Jobs");

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
                name: "Item");

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
