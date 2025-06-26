using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Innovision.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBetTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrawResults_Game_GameId",
                table: "DrawResults");

            migrationBuilder.DropForeignKey(
                name: "FK_JackpotWinner_OrderItem_OrderItemId",
                table: "JackpotWinner");

            migrationBuilder.DropForeignKey(
                name: "FK_LiveStream_Branch_BranchId",
                table: "LiveStream");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrawResults",
                table: "DrawResults");

            migrationBuilder.RenameTable(
                name: "DrawResults",
                newName: "DrawResult");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "JackpotWinner",
                newName: "BetTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_JackpotWinner_OrderItemId",
                table: "JackpotWinner",
                newName: "IX_JackpotWinner_BetTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_DrawResults_GameId",
                table: "DrawResult",
                newName: "IX_DrawResult_GameId");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "LiveStream",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "LiveStream",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "RoundReference",
                table: "DrawResult",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrawResult",
                table: "DrawResult",
                column: "DrawResultId");

            migrationBuilder.CreateTable(
                name: "BetTransaction",
                columns: table => new
                {
                    BetTransactionId = table.Column<long>(type: "bigint", nullable: false),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: false),
                    DrawResultId = table.Column<long>(type: "bigint", nullable: true),
                    RoundReference = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    BetValue = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    TransactionType = table.Column<string>(type: "text", nullable: false, defaultValue: "Regular"),
                    AmountBet = table.Column<decimal>(type: "numeric", nullable: false),
                    IsBonus = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    WinAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    VoidTransaction = table.Column<bool>(type: "boolean", nullable: false),
                    VoidTransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetTransaction", x => x.BetTransactionId);
                    table.ForeignKey(
                        name: "FK_BetTransaction_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetTransaction_DrawResult_BetTransactionId",
                        column: x => x.BetTransactionId,
                        principalTable: "DrawResult",
                        principalColumn: "DrawResultId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetTransaction_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameAppVersionStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameAppVersionStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "GameApplicationVersion",
                columns: table => new
                {
                    GameAppVersionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    ForceRefresh = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ReleaseNotes = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameApplicationVersion", x => x.GameAppVersionId);
                    table.ForeignKey(
                        name: "FK_GameApplicationVersion_GameAppVersionStatus_Status",
                        column: x => x.Status,
                        principalTable: "GameAppVersionStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameApplicationVersion_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiveStream_GameId",
                table: "LiveStream",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_BetTransaction_AccountInfoId",
                table: "BetTransaction",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_BetTransaction_GameId",
                table: "BetTransaction",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameApplicationVersion_GameId",
                table: "GameApplicationVersion",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameApplicationVersion_Status",
                table: "GameApplicationVersion",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_DrawResult_Game_GameId",
                table: "DrawResult",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JackpotWinner_BetTransaction_BetTransactionId",
                table: "JackpotWinner",
                column: "BetTransactionId",
                principalTable: "BetTransaction",
                principalColumn: "BetTransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiveStream_Branch_BranchId",
                table: "LiveStream",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiveStream_Game_GameId",
                table: "LiveStream",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrawResult_Game_GameId",
                table: "DrawResult");

            migrationBuilder.DropForeignKey(
                name: "FK_JackpotWinner_BetTransaction_BetTransactionId",
                table: "JackpotWinner");

            migrationBuilder.DropForeignKey(
                name: "FK_LiveStream_Branch_BranchId",
                table: "LiveStream");

            migrationBuilder.DropForeignKey(
                name: "FK_LiveStream_Game_GameId",
                table: "LiveStream");

            migrationBuilder.DropTable(
                name: "BetTransaction");

            migrationBuilder.DropTable(
                name: "GameApplicationVersion");

            migrationBuilder.DropTable(
                name: "GameAppVersionStatus");

            migrationBuilder.DropIndex(
                name: "IX_LiveStream_GameId",
                table: "LiveStream");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrawResult",
                table: "DrawResult");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "LiveStream");

            migrationBuilder.RenameTable(
                name: "DrawResult",
                newName: "DrawResults");

            migrationBuilder.RenameColumn(
                name: "BetTransactionId",
                table: "JackpotWinner",
                newName: "OrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_JackpotWinner_BetTransactionId",
                table: "JackpotWinner",
                newName: "IX_JackpotWinner_OrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DrawResult_GameId",
                table: "DrawResults",
                newName: "IX_DrawResults_GameId");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "LiveStream",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoundReference",
                table: "DrawResults",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrawResults",
                table: "DrawResults",
                column: "DrawResultId");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    CommissionStatusId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsBonus = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalNoOfItems = table.Column<int>(type: "integer", nullable: false),
                    TransactionNo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    AmountBet = table.Column<decimal>(type: "numeric", nullable: false),
                    BetItemType = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CompanyGameId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DrawDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DrawTime = table.Column<string>(type: "text", nullable: true),
                    ExcessAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    HasExcessAmount = table.Column<bool>(type: "boolean", nullable: false),
                    IsBonus = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    Used = table.Column<bool>(type: "boolean", nullable: false),
                    UsedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Values = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountInfoId",
                table: "Order",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_GameId",
                table: "Order",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_AccountInfoId",
                table: "OrderItem",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrawResults_Game_GameId",
                table: "DrawResults",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JackpotWinner_OrderItem_OrderItemId",
                table: "JackpotWinner",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiveStream_Branch_BranchId",
                table: "LiveStream",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
