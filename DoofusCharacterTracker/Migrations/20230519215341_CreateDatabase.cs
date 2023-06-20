using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoofusCharacterTracker.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Class = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    AlmanaxProgress = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UnlockedMoonIsland = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnlockedWabbitIsland = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnlockedPandala = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnlockedFrigost = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnlockedOtomaiIsland = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnlockedOtomaiBridgeOfDeath = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnlockedOhwymi = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Element = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterElements_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DungeonNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DungeonName = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DungeonNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DungeonNotes_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Profession = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessionNotes_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterElements_CharacterId",
                table: "CharacterElements",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_DungeonNotes_CharacterId",
                table: "DungeonNotes",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionNotes_CharacterId",
                table: "ProfessionNotes",
                column: "CharacterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterElements");

            migrationBuilder.DropTable(
                name: "DungeonNotes");

            migrationBuilder.DropTable(
                name: "ProfessionNotes");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
