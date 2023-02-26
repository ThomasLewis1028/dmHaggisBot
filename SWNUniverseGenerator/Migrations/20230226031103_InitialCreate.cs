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
                name: "Universes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GridX = table.Column<int>(type: "INTEGER", nullable: false),
                    GridY = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Alien",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    BodyTraits = table.Column<string>(type: "TEXT", nullable: true),
                    Lenses = table.Column<string>(type: "TEXT", nullable: true),
                    SocialStructures = table.Column<string>(type: "TEXT", nullable: true),
                    MultiPolarType = table.Column<string>(type: "TEXT", nullable: true),
                    UniverseName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alien_Universes_UniverseName",
                        column: x => x.UniverseName,
                        principalTable: "Universes",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    First = table.Column<string>(type: "TEXT", nullable: true),
                    Last = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    BirthPlanet = table.Column<string>(type: "TEXT", nullable: true),
                    CurrentLocation = table.Column<string>(type: "TEXT", nullable: true),
                    HairStyle = table.Column<string>(type: "TEXT", nullable: true),
                    HairCol = table.Column<string>(type: "TEXT", nullable: true),
                    EyeCol = table.Column<string>(type: "TEXT", nullable: true),
                    SkinCol = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    CrimeChance = table.Column<int>(type: "INTEGER", nullable: false),
                    ShipId = table.Column<string>(type: "TEXT", nullable: true),
                    InitialReaction = table.Column<string>(type: "TEXT", nullable: true),
                    UniverseName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Universes_UniverseName",
                        column: x => x.UniverseName,
                        principalTable: "Universes",
                        principalColumn: "Name");
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
                    UniverseName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problems_Universes_UniverseName",
                        column: x => x.UniverseName,
                        principalTable: "Universes",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Hull = table.Column<string>(type: "TEXT", nullable: true),
                    CaptainId = table.Column<string>(type: "TEXT", nullable: true),
                    PilotId = table.Column<string>(type: "TEXT", nullable: true),
                    EngineerId = table.Column<string>(type: "TEXT", nullable: true),
                    CommsId = table.Column<string>(type: "TEXT", nullable: true),
                    GunnerId = table.Column<string>(type: "TEXT", nullable: true),
                    CrewSkill = table.Column<int>(type: "INTEGER", nullable: false),
                    Cp = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeId = table.Column<string>(type: "TEXT", nullable: true),
                    LocationId = table.Column<string>(type: "TEXT", nullable: true),
                    UniverseName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ships_Universes_UniverseName",
                        column: x => x.UniverseName,
                        principalTable: "Universes",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Stars",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    StarColor = table.Column<int>(type: "INTEGER", nullable: false),
                    StarClass = table.Column<int>(type: "INTEGER", nullable: false),
                    UniverseName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stars_Universes_UniverseName",
                        column: x => x.UniverseName,
                        principalTable: "Universes",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false),
                    StarId = table.Column<string>(type: "TEXT", nullable: true),
                    UniverseName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Universes_UniverseName",
                        column: x => x.UniverseName,
                        principalTable: "Universes",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "ShipDefense",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ShipId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipDefense", x => x.Name);
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
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ShipId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipFitting", x => x.Name);
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
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ShipId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipWeapon", x => x.Name);
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
                    StarId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Contact = table.Column<string>(type: "TEXT", nullable: true),
                    UniverseName = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planets_Universes_UniverseName",
                        column: x => x.UniverseName,
                        principalTable: "Universes",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Planets_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
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
                    StarId = table.Column<string>(type: "TEXT", nullable: true),
                    UniverseName = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsOfInterest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointsOfInterest_Universes_UniverseName",
                        column: x => x.UniverseName,
                        principalTable: "Universes",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_PointsOfInterest_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
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
                    UniverseName = table.Column<string>(type: "TEXT", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Jobs_Universes_UniverseName",
                        column: x => x.UniverseName,
                        principalTable: "Universes",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alien_UniverseName",
                table: "Alien",
                column: "UniverseName");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UniverseName",
                table: "Characters",
                column: "UniverseName");

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
                name: "IX_Jobs_UniverseName",
                table: "Jobs",
                column: "UniverseName");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_UniverseName",
                table: "Planets",
                column: "UniverseName");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_ZoneId",
                table: "Planets",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsOfInterest_UniverseName",
                table: "PointsOfInterest",
                column: "UniverseName");

            migrationBuilder.CreateIndex(
                name: "IX_PointsOfInterest_ZoneId",
                table: "PointsOfInterest",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_UniverseName",
                table: "Problems",
                column: "UniverseName");

            migrationBuilder.CreateIndex(
                name: "IX_ShipDefense_ShipId",
                table: "ShipDefense",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipFitting_ShipId",
                table: "ShipFitting",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_UniverseName",
                table: "Ships",
                column: "UniverseName");

            migrationBuilder.CreateIndex(
                name: "IX_ShipWeapon_ShipId",
                table: "ShipWeapon",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_UniverseName",
                table: "Stars",
                column: "UniverseName");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_UniverseName",
                table: "Zones",
                column: "UniverseName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alien");

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
                name: "Planets");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Universes");
        }
    }
}
