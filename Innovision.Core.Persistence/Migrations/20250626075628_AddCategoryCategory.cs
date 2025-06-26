using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Innovision.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JackpotWinner_GameType_GameTypeId",
                table: "JackpotWinner");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_GameType_GameTypeId",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "GameDrawTypes");

            migrationBuilder.DropTable(
                name: "GameType");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_GameTypeId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_JackpotWinner_GameTypeId",
                table: "JackpotWinner");

            migrationBuilder.DropColumn(
                name: "GameTypeId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "GameTypeId",
                table: "JackpotWinner");

            migrationBuilder.RenameColumn(
                name: "StandardMissedDraws",
                table: "Game",
                newName: "GameStatusId");

            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Game",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalGameId",
                table: "Game",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameCategoryId",
                table: "Game",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameProviderId",
                table: "Game",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsInternal",
                table: "Game",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DrawResults",
                columns: table => new
                {
                    DrawResultId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoundId = table.Column<long>(type: "bigint", nullable: false),
                    RoundReference = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    StartCutoff = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndCutoff = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartCutoffEpoch = table.Column<long>(type: "bigint", nullable: false),
                    EndCutoffEpoch = table.Column<long>(type: "bigint", nullable: false),
                    BettingTime = table.Column<int>(type: "integer", nullable: false),
                    NoOfWinners = table.Column<int>(type: "integer", nullable: false),
                    WinAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalBetCount = table.Column<int>(type: "integer", nullable: false),
                    TotalBetAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    DrawDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawResults", x => x.DrawResultId);
                    table.ForeignKey(
                        name: "FK_DrawResults_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameCategory",
                columns: table => new
                {
                    GameCategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CoverImage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategory", x => x.GameCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "GameStatus",
                columns: table => new
                {
                    GameStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatus", x => x.GameStatusId);
                });

            migrationBuilder.CreateTable(
                name: "GameCatalogs",
                columns: table => new
                {
                    GameCatalogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    GameCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCatalogs", x => x.GameCatalogId);
                    table.ForeignKey(
                        name: "FK_GameCatalogs_GameCategory_GameCategoryId",
                        column: x => x.GameCategoryId,
                        principalTable: "GameCategory",
                        principalColumn: "GameCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCatalogs_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameProvider",
                columns: table => new
                {
                    GameProviderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameProviderUuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CoverImage = table.Column<string>(type: "text", nullable: true),
                    IsExternal = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Configuration = table.Column<string>(type: "text", nullable: true),
                    GameCategoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameProvider", x => x.GameProviderId);
                    table.ForeignKey(
                        name: "FK_GameProvider_GameCategory_GameCategoryId",
                        column: x => x.GameCategoryId,
                        principalTable: "GameCategory",
                        principalColumn: "GameCategoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameCategoryId",
                table: "Game",
                column: "GameCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameProviderId",
                table: "Game",
                column: "GameProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameStatusId",
                table: "Game",
                column: "GameStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawResults_GameId",
                table: "DrawResults",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCatalogs_GameCategoryId",
                table: "GameCatalogs",
                column: "GameCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCatalogs_GameId",
                table: "GameCatalogs",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameProvider_GameCategoryId",
                table: "GameProvider",
                column: "GameCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_GameCategory_GameCategoryId",
                table: "Game",
                column: "GameCategoryId",
                principalTable: "GameCategory",
                principalColumn: "GameCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_GameProvider_GameProviderId",
                table: "Game",
                column: "GameProviderId",
                principalTable: "GameProvider",
                principalColumn: "GameProviderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_GameStatus_GameStatusId",
                table: "Game",
                column: "GameStatusId",
                principalTable: "GameStatus",
                principalColumn: "GameStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_GameCategory_GameCategoryId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_GameProvider_GameProviderId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_GameStatus_GameStatusId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "DrawResults");

            migrationBuilder.DropTable(
                name: "GameCatalogs");

            migrationBuilder.DropTable(
                name: "GameProvider");

            migrationBuilder.DropTable(
                name: "GameStatus");

            migrationBuilder.DropTable(
                name: "GameCategory");

            migrationBuilder.DropIndex(
                name: "IX_Game_GameCategoryId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_GameProviderId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_GameStatusId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ExternalGameId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GameCategoryId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GameProviderId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "IsInternal",
                table: "Game");

            migrationBuilder.RenameColumn(
                name: "GameStatusId",
                table: "Game",
                newName: "StandardMissedDraws");

            migrationBuilder.AddColumn<int>(
                name: "GameTypeId",
                table: "OrderItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameTypeId",
                table: "JackpotWinner",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GameType",
                columns: table => new
                {
                    GameTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    CardPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    GameReferenceId = table.Column<int>(type: "integer", nullable: false),
                    GameTypeDesciption = table.Column<string>(type: "text", nullable: true),
                    GameTypeName = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameType", x => x.GameTypeId);
                    table.ForeignKey(
                        name: "FK_GameType_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameDrawTypes",
                columns: table => new
                {
                    GameDrawTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameTypeId = table.Column<int>(type: "integer", nullable: false),
                    DrawSchedule = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DrawTypeName = table.Column<string>(type: "text", nullable: false),
                    EndCutOff = table.Column<TimeSpan>(type: "interval", nullable: false),
                    StartCutOff = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDrawTypes", x => x.GameDrawTypeId);
                    table.ForeignKey(
                        name: "FK_GameDrawTypes_GameType_GameTypeId",
                        column: x => x.GameTypeId,
                        principalTable: "GameType",
                        principalColumn: "GameTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_GameTypeId",
                table: "OrderItem",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JackpotWinner_GameTypeId",
                table: "JackpotWinner",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDrawTypes_GameTypeId",
                table: "GameDrawTypes",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GameType_GameId",
                table: "GameType",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_JackpotWinner_GameType_GameTypeId",
                table: "JackpotWinner",
                column: "GameTypeId",
                principalTable: "GameType",
                principalColumn: "GameTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_GameType_GameTypeId",
                table: "OrderItem",
                column: "GameTypeId",
                principalTable: "GameType",
                principalColumn: "GameTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
