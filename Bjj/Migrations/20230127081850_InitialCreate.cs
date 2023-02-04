using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjj.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fighter",
                columns: table => new
                {
                    FighterId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WeightCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    BeltColour = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fighter", x => x.FighterId);
                });

            migrationBuilder.CreateTable(
                name: "FightResultBy",
                columns: table => new
                {
                    FightResultById = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightResultBy", x => x.FightResultById);
                });

            migrationBuilder.CreateTable(
                name: "Fight",
                columns: table => new
                {
                    Fighter1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Fighter2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    WinnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    FightResultById = table.Column<int>(type: "INTEGER", nullable: false),
                    FightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateOfFight = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WeightCategory = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fight", x => x.FightId);
                    table.ForeignKey(
                        name: "FK_Fight_FightResultBy_FightResultById",
                        column: x => x.FightResultById,
                        principalTable: "Fighter",
                        principalColumn: "FightResultById",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fight_Fighter_Fighter1Id",
                        column: x => x.Fighter1Id,
                        principalTable: "Fighter",
                        principalColumn: "FighterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fight_Fighter_Fighter2Id",
                        column: x => x.Fighter2Id,
                        principalTable: "Fighter",
                        principalColumn: "FighterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fight_Fighter_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Fighter",
                        principalColumn: "FighterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fight_Fighter1Id",
                table: "Fight",
                column: "Fighter1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_Fighter2Id",
                table: "Fight",
                column: "Fighter2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_FightResultById",
                table: "Fight",
                column: "FightResultById");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_WinnerId",
                table: "Fight",
                column: "WinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fight");

            migrationBuilder.DropTable(
                name: "FightResultBy");

            migrationBuilder.DropTable(
                name: "Fighter");
        }
    }
}
