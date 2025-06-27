using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innovision.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class suffix_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Suffix",
                table: "AccountInfo",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "AccountInfo");
        }
    }
}
