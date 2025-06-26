using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Innovision.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barangay",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Municipality",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PermanentBarangay",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PermanentMunicipality",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PermanentProvince",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PermanentRegion",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PermanentStreetOrPurok",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PresentBarangay",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PresentMunicipality",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PresentProvince",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PresentRegion",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PresentStreetOrPurok",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "StreetOrPurok",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Barangay",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "Municipality",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PermanentBarangay",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PermanentMunicipality",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PermanentProvince",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PermanentRegion",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PermanentStreetOrPurok",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PresentBarangay",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PresentMunicipality",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PresentProvince",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PresentRegion",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PresentStreetOrPurok",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "StreetOrPurok",
                table: "AccountInfo");

            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "Branch",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "AccountInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PermanentAddressId",
                table: "AccountInfo",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PresentAddressId",
                table: "AccountInfo",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScreenName",
                table: "AccountInfo",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Municipality = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Barangay = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    StreetOrPurok = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PresentRegion = table.Column<string>(type: "text", nullable: true),
                    PresentProvince = table.Column<string>(type: "text", nullable: true),
                    PresentMunicipality = table.Column<string>(type: "text", nullable: true),
                    PresentBarangay = table.Column<string>(type: "text", nullable: true),
                    PresentStreetOrPurok = table.Column<string>(type: "text", nullable: true),
                    PermanentRegion = table.Column<string>(type: "text", nullable: true),
                    PermanentProvince = table.Column<string>(type: "text", nullable: true),
                    PermanentMunicipality = table.Column<string>(type: "text", nullable: true),
                    PermanentBarangay = table.Column<string>(type: "text", nullable: true),
                    PermanentStreetOrPurok = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_AddressId",
                table: "Branch",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfo_PermanentAddressId",
                table: "AccountInfo",
                column: "PermanentAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfo_PresentAddressId",
                table: "AccountInfo",
                column: "PresentAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountInfo_Address_PermanentAddressId",
                table: "AccountInfo",
                column: "PermanentAddressId",
                principalTable: "Address",
                principalColumn: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountInfo_Address_PresentAddressId",
                table: "AccountInfo",
                column: "PresentAddressId",
                principalTable: "Address",
                principalColumn: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Address_AddressId",
                table: "Branch",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountInfo_Address_PermanentAddressId",
                table: "AccountInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountInfo_Address_PresentAddressId",
                table: "AccountInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Address_AddressId",
                table: "Branch");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Branch_AddressId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_AccountInfo_PermanentAddressId",
                table: "AccountInfo");

            migrationBuilder.DropIndex(
                name: "IX_AccountInfo_PresentAddressId",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PermanentAddressId",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "PresentAddressId",
                table: "AccountInfo");

            migrationBuilder.DropColumn(
                name: "ScreenName",
                table: "AccountInfo");

            migrationBuilder.AddColumn<string>(
                name: "Barangay",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentBarangay",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentMunicipality",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentProvince",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentRegion",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentStreetOrPurok",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentBarangay",
                table: "Branch",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentMunicipality",
                table: "Branch",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentProvince",
                table: "Branch",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentRegion",
                table: "Branch",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentStreetOrPurok",
                table: "Branch",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetOrPurok",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Barangay",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentBarangay",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentMunicipality",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentProvince",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentRegion",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentStreetOrPurok",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentBarangay",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentMunicipality",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentProvince",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentRegion",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentStreetOrPurok",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetOrPurok",
                table: "AccountInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
