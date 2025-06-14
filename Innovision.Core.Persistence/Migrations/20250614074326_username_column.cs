using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innovision.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class username_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AccountInfo",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AccountInfo");
        }
    }
}
