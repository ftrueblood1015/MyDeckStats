using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDeckStats.Migrations
{
    /// <inheritdoc />
    public partial class CardEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MtgCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OracleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScryfallUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    manaCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConvertedManaCost = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OracleText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Power = table.Column<int>(type: "int", nullable: true),
                    Toughness = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EdhrecRank = table.Column<int>(type: "int", nullable: false),
                    PennyRank = table.Column<int>(type: "int", nullable: false),
                    ProducesMana = table.Column<bool>(type: "bit", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MtgCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MtgCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardTypes_MtgCards_MtgCardId",
                        column: x => x.MtgCardId,
                        principalTable: "MtgCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColorIdentities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MtgCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorIdentities_MtgCards_MtgCardId",
                        column: x => x.MtgCardId,
                        principalTable: "MtgCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MtgKeywords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MtgCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MtgKeywords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MtgKeywords_MtgCards_MtgCardId",
                        column: x => x.MtgCardId,
                        principalTable: "MtgCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MtgCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MtgSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetCards_MtgCards_MtgCardId",
                        column: x => x.MtgCardId,
                        principalTable: "MtgCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetCards_MtgSets_MtgSetId",
                        column: x => x.MtgSetId,
                        principalTable: "MtgSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardTypes_MtgCardId",
                table: "CardTypes",
                column: "MtgCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorIdentities_MtgCardId",
                table: "ColorIdentities",
                column: "MtgCardId");

            migrationBuilder.CreateIndex(
                name: "IX_MtgKeywords_MtgCardId",
                table: "MtgKeywords",
                column: "MtgCardId");

            migrationBuilder.CreateIndex(
                name: "IX_SetCards_MtgCardId",
                table: "SetCards",
                column: "MtgCardId");

            migrationBuilder.CreateIndex(
                name: "IX_SetCards_MtgSetId",
                table: "SetCards",
                column: "MtgSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropTable(
                name: "ColorIdentities");

            migrationBuilder.DropTable(
                name: "MtgKeywords");

            migrationBuilder.DropTable(
                name: "SetCards");

            migrationBuilder.DropTable(
                name: "MtgCards");
        }
    }
}
