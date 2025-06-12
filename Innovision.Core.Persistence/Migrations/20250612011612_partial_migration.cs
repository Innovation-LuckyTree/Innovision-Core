using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Innovision.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class partial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Audit");

            migrationBuilder.CreateTable(
                name: "AuditLog",
                schema: "Audit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    KeyValues = table.Column<string>(type: "text", nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: false),
                    NewValues = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankReference",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankReference", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchCreditObjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    BranchBonusObjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    BranchName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    BranchCode = table.Column<string>(type: "text", nullable: true),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    GameSiteManagerId = table.Column<long>(type: "bigint", nullable: true),
                    GameSiteAccountId = table.Column<long>(type: "bigint", nullable: true),
                    DefaultAccountId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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
                    PermanentRegion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PermanentProvince = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PermanentMunicipality = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PermanentBarangay = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PermanentStreetOrPurok = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "DepositStatus",
                columns: table => new
                {
                    DepositStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositStatus", x => x.DepositStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    StandardMissedDraws = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "JackpotWinnerStatus",
                columns: table => new
                {
                    JackpotWinnerStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JackpotWinnerStatus", x => x.JackpotWinnerStatusId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    NotificationTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.NotificationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "OTP",
                columns: table => new
                {
                    OtpID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MobileNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    IsVerify = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TransType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpireDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP", x => x.OtpID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "QuarantineKafka",
                columns: table => new
                {
                    QuarantineKafkaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KafkaValue = table.Column<string>(type: "text", nullable: false),
                    KafkaTopic = table.Column<string>(type: "text", nullable: false),
                    Attempts = table.Column<int>(type: "integer", nullable: true),
                    ErrorCode = table.Column<string>(type: "text", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    AttemptedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CompletedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuarantineKafka", x => x.QuarantineKafkaId);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    UserTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserTypeName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    GroupType = table.Column<int>(type: "integer", nullable: false),
                    RoleType = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "WalletSetting",
                columns: table => new
                {
                    WalletSettingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InitialMinimumDeposit = table.Column<decimal>(type: "numeric", nullable: false),
                    SubsequentMinimumDeposit = table.Column<decimal>(type: "numeric", nullable: false),
                    MaximumDepositAtOnce = table.Column<decimal>(type: "numeric", nullable: false),
                    MaximumDepositPerDay = table.Column<decimal>(type: "numeric", nullable: false),
                    InitialMinimumWithdraw = table.Column<decimal>(type: "numeric", nullable: false),
                    SubsequentMinimumWithdraw = table.Column<decimal>(type: "numeric", nullable: false),
                    MaximumWithdrawAtOnce = table.Column<decimal>(type: "numeric", nullable: false),
                    MaximumWithdrawPerDay = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxPercentage = table.Column<int>(type: "integer", nullable: false),
                    TaxableAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletSetting", x => x.WalletSettingId);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalStatus",
                columns: table => new
                {
                    WithdrawalStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalStatus", x => x.WithdrawalStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Announcement",
                columns: table => new
                {
                    AnnouncementId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SendTo = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsBanner = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.AnnouncementId);
                    table.ForeignKey(
                        name: "FK_Announcement_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiveStream",
                columns: table => new
                {
                    LiveStreamId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveStream", x => x.LiveStreamId);
                    table.ForeignKey(
                        name: "FK_LiveStream_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FrequentlyAskQuestion",
                columns: table => new
                {
                    FrequentlyAskQuestionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    IsApplicationRelated = table.Column<int>(type: "integer", nullable: false),
                    OrderNo = table.Column<int>(type: "integer", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequentlyAskQuestion", x => x.FrequentlyAskQuestionId);
                    table.ForeignKey(
                        name: "FK_FrequentlyAskQuestion_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameType",
                columns: table => new
                {
                    GameTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    GameReferenceId = table.Column<int>(type: "integer", nullable: false),
                    GameTypeName = table.Column<string>(type: "text", nullable: true),
                    GameTypeDesciption = table.Column<string>(type: "text", nullable: true),
                    CardPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
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
                name: "AccountInfo",
                columns: table => new
                {
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountCreditId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountBonusId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    MartialStatus = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    BloodType = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Nationality = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NatureOfWork = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    SourceOfIncome = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    BirthDate = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    MobileNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Commision = table.Column<decimal>(type: "numeric(10,4)", nullable: false),
                    UserTypeId = table.Column<int>(type: "integer", nullable: false),
                    FmTypeId = table.Column<int>(type: "integer", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    RefferralKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    AccountStatusId = table.Column<int>(type: "integer", nullable: false),
                    SalaryRange = table.Column<int>(type: "integer", nullable: true),
                    RefferralCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ValidId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FrontIdPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    BackIdPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SignaturePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ProfilePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SelfiePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    AccountCommission = table.Column<string>(type: "character varying(2500)", maxLength: 2500, nullable: true),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeclined = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PaymentAccountId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    LastSetPassword = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ForVerification = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Municipality = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Barangay = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    StreetOrPurok = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PresentRegion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PresentProvince = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PresentMunicipality = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PresentBarangay = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PresentStreetOrPurok = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PermanentRegion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PermanentProvince = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PermanentMunicipality = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PermanentBarangay = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PermanentStreetOrPurok = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfo", x => x.AccountInfoId);
                    table.ForeignKey(
                        name: "FK_AccountInfo_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountInfo_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTypeConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsMainUser = table.Column<bool>(type: "boolean", nullable: false),
                    RequestLevel = table.Column<int>(type: "integer", nullable: true),
                    CashInLevel = table.Column<int>(type: "integer", nullable: true),
                    RequestCredit = table.Column<string>(type: "text", nullable: true),
                    CashinDeposit = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypeConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTypeConfig_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameDrawTypes",
                columns: table => new
                {
                    GameDrawTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameTypeId = table.Column<int>(type: "integer", nullable: false),
                    DrawTypeName = table.Column<string>(type: "text", nullable: false),
                    DrawSchedule = table.Column<TimeSpan>(type: "interval", nullable: false),
                    StartCutOff = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndCutOff = table.Column<TimeSpan>(type: "interval", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AccountHistory",
                columns: table => new
                {
                    AccountHistoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHistory", x => x.AccountHistoryId);
                    table.ForeignKey(
                        name: "FK_AccountHistory_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountSetting",
                columns: table => new
                {
                    AccountSettingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    InAppNotification = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    SmsNotification = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    EmailNotification = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSetting", x => x.AccountSettingId);
                    table.ForeignKey(
                        name: "FK_AccountSetting_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressCode",
                columns: table => new
                {
                    AddressCodeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    RegionCode = table.Column<string>(type: "text", nullable: true),
                    ProvinceCode = table.Column<string>(type: "text", nullable: true),
                    MunicipalityCode = table.Column<string>(type: "text", nullable: true),
                    BarangayCode = table.Column<string>(type: "text", nullable: true),
                    PermRegionCode = table.Column<string>(type: "text", nullable: true),
                    PermProvinceCode = table.Column<string>(type: "text", nullable: true),
                    PermMunicipalityCode = table.Column<string>(type: "text", nullable: true),
                    PermBarangayCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressCode", x => x.AddressCodeId);
                    table.ForeignKey(
                        name: "FK_AddressCode_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdministrativeExclusion",
                columns: table => new
                {
                    AdministrativeExclusionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    DayDuration = table.Column<int>(type: "integer", nullable: false),
                    TimeDuration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DateExpiry = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeExclusion", x => x.AdministrativeExclusionId);
                    table.ForeignKey(
                        name: "FK_AdministrativeExclusion_AccountInfo_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlockedUserHistory",
                columns: table => new
                {
                    BlockedUserHistoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    BlockedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Remarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedUserHistory", x => x.BlockedUserHistoryId);
                    table.ForeignKey(
                        name: "FK_BlockedUserHistory_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    DepositId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TransactionType = table.Column<string>(type: "text", nullable: false),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    DepositStatusId = table.Column<int>(type: "integer", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    TransactionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.DepositId);
                    table.ForeignKey(
                        name: "FK_Deposit_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposit_DepositStatus_DepositStatusId",
                        column: x => x.DepositStatusId,
                        principalTable: "DepositStatus",
                        principalColumn: "DepositStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposit_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RedirectUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_NotificationType_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationType",
                        principalColumn: "NotificationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionNo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalNoOfItems = table.Column<int>(type: "integer", nullable: false),
                    CommissionStatusId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsBonus = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
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
                name: "PlayerActivity",
                columns: table => new
                {
                    ActivityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    MissedDraws = table.Column<int>(type: "integer", nullable: false),
                    Extended = table.Column<int>(type: "integer", nullable: false),
                    RequiredTopay = table.Column<bool>(type: "boolean", nullable: false),
                    ExcludeDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastDrawDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastDrawTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerActivity", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_PlayerActivity_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelfExclusion",
                columns: table => new
                {
                    SelfExclusionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    IsIndefinite = table.Column<bool>(type: "boolean", nullable: false),
                    DateStart = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DateEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfExclusion", x => x.SelfExclusionId);
                    table.ForeignKey(
                        name: "FK_SelfExclusion_AccountInfo_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelfLimit",
                columns: table => new
                {
                    SelfLimitId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    AmountLimit = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfLimit", x => x.SelfLimitId);
                    table.ForeignKey(
                        name: "FK_SelfLimit_AccountInfo_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStatus",
                columns: table => new
                {
                    UserStatusId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    SubStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatus", x => x.UserStatusId);
                    table.ForeignKey(
                        name: "FK_UserStatus_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawal",
                columns: table => new
                {
                    TransactionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TransactionType = table.Column<string>(type: "text", nullable: false),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    BankReferenceId = table.Column<int>(type: "integer", nullable: true),
                    BankInfo = table.Column<string>(type: "text", nullable: true),
                    ImageProof = table.Column<string>(type: "text", nullable: true),
                    TransactionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    NotificationStatus = table.Column<int>(type: "integer", nullable: false, defaultValue: -1),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawal", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Withdrawal_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Withdrawal_BankReference_BankReferenceId",
                        column: x => x.BankReferenceId,
                        principalTable: "BankReference",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Withdrawal_WithdrawalStatus_Status",
                        column: x => x.Status,
                        principalTable: "WithdrawalStatus",
                        principalColumn: "WithdrawalStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Used = table.Column<bool>(type: "boolean", nullable: false),
                    Values = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    GameTypeId = table.Column<int>(type: "integer", nullable: false),
                    BetItemType = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CompanyGameId = table.Column<int>(type: "integer", nullable: false),
                    AmountBet = table.Column<decimal>(type: "numeric", nullable: false),
                    ExcessAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsBonus = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    HasExcessAmount = table.Column<bool>(type: "boolean", nullable: false),
                    UsedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DrawTime = table.Column<string>(type: "text", nullable: true),
                    DrawDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
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
                        name: "FK_OrderItem_GameType_GameTypeId",
                        column: x => x.GameTypeId,
                        principalTable: "GameType",
                        principalColumn: "GameTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JackpotWinner",
                columns: table => new
                {
                    JackpotWinnerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyGameId = table.Column<int>(type: "integer", nullable: false),
                    TransactionNo = table.Column<string>(type: "text", nullable: false),
                    BetValue = table.Column<string>(type: "text", nullable: false),
                    DrawResultId = table.Column<long>(type: "bigint", nullable: false),
                    GameTypeId = table.Column<int>(type: "integer", nullable: false),
                    GameTypeName = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    DrawResult = table.Column<string>(type: "text", nullable: false),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false),
                    GameScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    DrawDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DrawTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    PrizePoolAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    NetWinAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    GrossWinAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    NumberOfWinners = table.Column<int>(type: "integer", nullable: false),
                    TotalBetAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    ApproverAccountId = table.Column<long>(type: "bigint", nullable: true),
                    ReleaserAccountId = table.Column<long>(type: "bigint", nullable: true),
                    JackpotWinnerStatusId = table.Column<int>(type: "integer", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JackpotWinner", x => x.JackpotWinnerId);
                    table.ForeignKey(
                        name: "FK_JackpotWinner_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JackpotWinner_AccountInfo_ApproverAccountId",
                        column: x => x.ApproverAccountId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId");
                    table.ForeignKey(
                        name: "FK_JackpotWinner_AccountInfo_ReleaserAccountId",
                        column: x => x.ReleaserAccountId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoId");
                    table.ForeignKey(
                        name: "FK_JackpotWinner_GameType_GameTypeId",
                        column: x => x.GameTypeId,
                        principalTable: "GameType",
                        principalColumn: "GameTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JackpotWinner_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JackpotWinner_JackpotWinnerStatus_JackpotWinnerStatusId",
                        column: x => x.JackpotWinnerStatusId,
                        principalTable: "JackpotWinnerStatus",
                        principalColumn: "JackpotWinnerStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JackpotWinner_OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "OrderItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JackpotWinnerAttachment",
                columns: table => new
                {
                    JackpotWinnerAttachmentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JackpotWinnerId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JackpotWinnerAttachment", x => x.JackpotWinnerAttachmentId);
                    table.ForeignKey(
                        name: "FK_JackpotWinnerAttachment_JackpotWinner_JackpotWinnerId",
                        column: x => x.JackpotWinnerId,
                        principalTable: "JackpotWinner",
                        principalColumn: "JackpotWinnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountHistory_AccountInfoId",
                table: "AccountHistory",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfo_BranchId",
                table: "AccountInfo",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfo_UserTypeId",
                table: "AccountInfo",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetting_AccountInfoId",
                table: "AccountSetting",
                column: "AccountInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressCode_AccountInfoId",
                table: "AddressCode",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeExclusion_AccountId",
                table: "AdministrativeExclusion",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_BranchId",
                table: "Announcement",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUserHistory_AccountInfoId",
                table: "BlockedUserHistory",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_AccountInfoId",
                table: "Deposit",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_DepositStatusId",
                table: "Deposit",
                column: "DepositStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_PaymentMethodId",
                table: "Deposit",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyAskQuestion_GameId",
                table: "FrequentlyAskQuestion",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDrawTypes_GameTypeId",
                table: "GameDrawTypes",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GameType_GameId",
                table: "GameType",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_JackpotWinner_AccountInfoId",
                table: "JackpotWinner",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_JackpotWinner_ApproverAccountId",
                table: "JackpotWinner",
                column: "ApproverAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JackpotWinner_GameId",
                table: "JackpotWinner",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_JackpotWinner_GameTypeId",
                table: "JackpotWinner",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JackpotWinner_JackpotWinnerStatusId",
                table: "JackpotWinner",
                column: "JackpotWinnerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JackpotWinner_OrderItemId",
                table: "JackpotWinner",
                column: "OrderItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JackpotWinner_ReleaserAccountId",
                table: "JackpotWinner",
                column: "ReleaserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JackpotWinnerAttachment_JackpotWinnerId",
                table: "JackpotWinnerAttachment",
                column: "JackpotWinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveStream_BranchId",
                table: "LiveStream",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AccountInfoId",
                table: "Notification",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationTypeId",
                table: "Notification",
                column: "NotificationTypeId");

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
                name: "IX_OrderItem_GameTypeId",
                table: "OrderItem",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerActivity_AccountInfoId",
                table: "PlayerActivity",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfExclusion_AccountId",
                table: "SelfExclusion",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfLimit_AccountId",
                table: "SelfLimit",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatus_AccountInfoId",
                table: "UserStatus",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypeConfig_UserTypeId",
                table: "UserTypeConfig",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_AccountInfoId",
                table: "Withdrawal",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_BankReferenceId",
                table: "Withdrawal",
                column: "BankReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_Status",
                table: "Withdrawal",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountHistory");

            migrationBuilder.DropTable(
                name: "AccountSetting");

            migrationBuilder.DropTable(
                name: "AddressCode");

            migrationBuilder.DropTable(
                name: "AdministrativeExclusion");

            migrationBuilder.DropTable(
                name: "Announcement");

            migrationBuilder.DropTable(
                name: "AuditLog",
                schema: "Audit");

            migrationBuilder.DropTable(
                name: "BlockedUserHistory");

            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "FrequentlyAskQuestion");

            migrationBuilder.DropTable(
                name: "GameDrawTypes");

            migrationBuilder.DropTable(
                name: "JackpotWinnerAttachment");

            migrationBuilder.DropTable(
                name: "LiveStream");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "OTP");

            migrationBuilder.DropTable(
                name: "PlayerActivity");

            migrationBuilder.DropTable(
                name: "QuarantineKafka");

            migrationBuilder.DropTable(
                name: "SelfExclusion");

            migrationBuilder.DropTable(
                name: "SelfLimit");

            migrationBuilder.DropTable(
                name: "UserStatus");

            migrationBuilder.DropTable(
                name: "UserTypeConfig");

            migrationBuilder.DropTable(
                name: "WalletSetting");

            migrationBuilder.DropTable(
                name: "Withdrawal");

            migrationBuilder.DropTable(
                name: "DepositStatus");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "JackpotWinner");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropTable(
                name: "BankReference");

            migrationBuilder.DropTable(
                name: "WithdrawalStatus");

            migrationBuilder.DropTable(
                name: "JackpotWinnerStatus");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "GameType");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "AccountInfo");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
