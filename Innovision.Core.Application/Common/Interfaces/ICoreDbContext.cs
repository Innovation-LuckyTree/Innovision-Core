using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Innovision.Core.Application.Interfaces;

public interface ICoreDbContext
{
    DbSet<Account> Accounts { get; set; }
    DbSet<Address> Addresses { get; set; }
    DbSet<Branch> Branches { get; set; }
    DbSet<UserType> UserTypes { get; set; }
    DbSet<Game> Games { get; set; }
    DbSet<GameAppVersion> GameAppVersions { get; set; }
    DbSet<GameAppVersionStatus> GameAppVersionStatuses { get; set; }
    DbSet<GameCatalog> GameCatalogs { get; set; }
    DbSet<GameCategory> GameCategories { get; set; }
    DbSet<GameProvider> GameProviders { get; set; }
    DbSet<GameStatus> GameStatuses { get; set; }
    DbSet<BetTransaction> BetTransactions { get; set; }
    DbSet<OTP> Otps { get; set; }
    DbSet<Withdrawal> Withdrawals { get; set; }
    DbSet<Deposit> Deposits { get; set; }
    DbSet<PaymentMethod> PaymentMethods { get; set; }
    DbSet<WalletSetting> WalletSettings { get; set; }
    DbSet<DepositStatus> DepositStatuses { get; set; }
    DbSet<UserStatus> UserStatuses { get; set; }
    DbSet<AccountHistory> AccountHistories { get; set; }
    DbSet<FrequentlyAskQuestion> FrequentlyAskQuestions { get; set; }
    DbSet<JackpotWinner> JackpotWinners { get; set; }
    DbSet<JackpotWinnerStatus> JackpotWinnerStatus { get; set; }
    DbSet<JackpotWinnerAttachment> JackpotWinnerAttachments { get; set; }
    DbSet<AddressCode> AddressCodes { get; set; }
    DbSet<BankReference> BankReferences { get; set; }
    DbSet<UserTypeConfig> UserTypeConfigs { get; set; }
    DbSet<Notification> Notifications { get; set; }
    DbSet<NotificationType> NotificationTypes { get; set; }
    DbSet<AdministrativeExclusion> AdministrativeExclusions { get; set; }
    DbSet<SelfLimit> SelfLimits { get; set; }
    DbSet<LiveStream> LiveStreams { get; set; }
    DbSet<Announcement> Announcements { get; set; }
    DbSet<PlayerActivity> PlayerActivities { get; set; }
    DbSet<BlockedUserHistory> BlockedUserHistories { get; set; }
    DbSet<QuarantineKafka> QuarantineKafkas { get; set; }
    DbSet<SelfExclusion> SelfExclusions { get; set; }
    DbSet<AuditLog> AuditLogs { get; set; }
    DbSet<DrawResult> DrawResults { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IQueryable<T> CreateQuery<T>(string sqlQuery) where T : class;
    IQueryable<T> CreateQuery<T>(string sqlQuery, params object[] parameters) where T : class;
    DatabaseFacade Database { get; }
}