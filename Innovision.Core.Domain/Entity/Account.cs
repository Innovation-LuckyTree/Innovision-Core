using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public class Account : AuditableEntity
{
    public Account()
    {
        Orders = [];
        OrderItems = [];
    }

    public long AccountInfoId { get; set; }
    public Guid AccountObjectId { get; set; }
    public Guid AccountCreditId { get; set; } = Guid.NewGuid();
    public Guid AccountBonusId { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string MartialStatus { get; set; }
    public string BloodType { get; set; }
    public string Nationality { get; set; }
    public string NatureOfWork { get; set; }
    public string SourceOfIncome { get; set; }
    public string PlaceOfBirth { get; set; }
    public string BirthDate { get; set; }
    public string MobileNumber { get; set; }
    public decimal Commision { get; set; }
    public int UserTypeId { get; set; }
    public int? FmTypeId { get; set; }
    public int BranchId { get; set; }
    public bool IsMain { get; set; }
    public string RefferralKey { get; set; }
    public bool IsActive { get; set; }
    public int AccountStatusId { get; set; }
    //public int? UserStatus { get; set; }
    public int? SalaryRange { get; set; }
    public string RefferralCode { get; set; }
    public string ValidId { get; set; }
    public string FrontIdPath { get; set; }
    public string BackIdPath { get; set; }
    public string SignaturePath { get; set; }
    public string ProfilePath { get; set; }
    public string SelfiePath { get; set; }
    public string AccountCommission { get; set; }
    public bool IsVerified { get; set; }
    public bool? IsDeclined { get; set; }
    public string Remarks { get; set; }
    public string PaymentAccountId { get; set; }
    public DateTime? LastSetPassword { get; set; }
    public bool ForVerification { get; set; }
    public string ScreenName { get; set; }
    public int Level { get; set; } = 1;
    public long? PresentAddressId { get; set; }
    public long? PermanentAddressId { get; set; }

    public virtual Address PresentAddress { get; set; }
    public virtual Address PermanentAddress { get; set; }
    public virtual Branch Branch { get; set; }
    public virtual UserType UserType { get; set; }
    public virtual AccountSetting AccountSetting { get; set; }
    public virtual IEnumerable<AddressCode> AddressCodes { get; set; }
    public virtual IEnumerable<UserStatus> UserStatuses { get; set; }
    public virtual IEnumerable<AccountHistory> AccountHistories { get; set; }

    public virtual IEnumerable<Order> Orders { get; set; }
    public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    public virtual IEnumerable<Withdrawal> Withdrawals { get; set; }
    public virtual IEnumerable<Deposit> Deposits { get; set; }
    public virtual ICollection<JackpotWinner> JackpotWinners { get; set; }
    public virtual ICollection<JackpotWinner> ApprovedJackpotWinners { get; set; }
    public virtual ICollection<JackpotWinner> ReleasedJackpotWinners { get; set; }
    public virtual IEnumerable<Notification> Notifications { get; set; }
    public virtual IEnumerable<SelfLimit> SelfLimits { get; set; }
    public virtual IEnumerable<SelfExclusion> SelfExclusions { get; set; }
    public virtual IEnumerable<AdministrativeExclusion> AdministrativeExclusions { get; set; }
    public virtual IEnumerable<PlayerActivity> PlayerActivities { get; set; }
    public virtual ICollection<BlockedUserHistory> BlockedUserHistories { get; set; }
}