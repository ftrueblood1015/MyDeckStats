using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDeckStats.Migrations
{
    /// <inheritdoc />
    public partial class AddKeywordsToCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "MtgCards",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "MtgCards");
        }
    }
}
