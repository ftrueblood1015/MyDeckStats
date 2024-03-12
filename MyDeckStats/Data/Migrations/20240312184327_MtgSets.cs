using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDeckStats.Data.Migrations
{
    /// <inheritdoc />
    public partial class MtgSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MtgSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScryfallUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleasedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SetType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardCount = table.Column<int>(type: "int", nullable: false),
                    ParentSetCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Digital = table.Column<bool>(type: "bit", nullable: false),
                    NonfoilOnly = table.Column<bool>(type: "bit", nullable: false),
                    FoilOnly = table.Column<bool>(type: "bit", nullable: false),
                    IconSvgUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScryfallId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MtgSets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MtgSets");
        }
    }
}
