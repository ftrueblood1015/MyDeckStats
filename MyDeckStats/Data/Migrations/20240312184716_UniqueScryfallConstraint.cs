using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDeckStats.Data.Migrations
{
    /// <inheritdoc />
    public partial class UniqueScryfallConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ScryfallId",
                table: "MtgSets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_MtgSets_ScryfallId",
                table: "MtgSets",
                column: "ScryfallId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MtgSets_ScryfallId",
                table: "MtgSets");

            migrationBuilder.AlterColumn<string>(
                name: "ScryfallId",
                table: "MtgSets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
