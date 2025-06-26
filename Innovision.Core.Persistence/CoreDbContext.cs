using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Innovision.Core.Persistence;

public class CoreDbContext : DbContext, ICoreDbContext
{
    public CoreDbContext(DbContextOptions<CoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GameDrawType> GameDrawTypes { get; set; }
    public DbSet<GameType> GameTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OTP> Otps { get; set; }
    public DbSet<Withdrawal> Withdrawals { get; set; }
    public DbSet<Deposit> Deposits { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<WalletSetting> WalletSettings { get; set; }
    public DbSet<DepositStatus> DepositStatuses { get; set; }
    public DbSet<UserStatus> UserStatuses { get; set; }
    public DbSet<AccountHistory> AccountHistories { get; set; }
    public DbSet<FrequentlyAskQuestion> FrequentlyAskQuestions { get; set; }
    public DbSet<JackpotWinner> JackpotWinners { get; set; }
    public DbSet<JackpotWinnerStatus> JackpotWinnerStatus { get; set; }
    public DbSet<JackpotWinnerAttachment> JackpotWinnerAttachments { get; set; }
    public DbSet<AddressCode> AddressCodes { get; set; }
    public DbSet<BankReference> BankReferences { get; set; }
    public DbSet<UserTypeConfig> UserTypeConfigs { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationType> NotificationTypes { get; set; }
    public DbSet<AdministrativeExclusion> AdministrativeExclusions { get; set; }
    public DbSet<SelfLimit> SelfLimits { get; set; }
    public DbSet<LiveStream> LiveStreams { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<PlayerActivity> PlayerActivities { get; set; }
    public DbSet<BlockedUserHistory> BlockedUserHistories { get; set; }
    public DbSet<QuarantineKafka> QuarantineKafkas { get; set; }
    public DbSet<SelfExclusion> SelfExclusions { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    public DatabaseFacade Database => base.Database;

    public IQueryable<T> CreateQuery<T>(string sqlQuer) where T : class
        => Set<T>().FromSqlRaw(sqlQuer).AsQueryable();

    public IQueryable<T> CreateQuery<T>(string sqlQuer, params object[] parameters) where T : class
        => Set<T>().FromSqlRaw(sqlQuer, parameters).AsQueryable();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<AuditLog>();

        modelBuilder.HasAnnotation("ProductVersion", "1.1.1-servicing");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreDbContext).Assembly);
    }
}
