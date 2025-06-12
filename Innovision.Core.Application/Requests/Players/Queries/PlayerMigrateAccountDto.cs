using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using AccStatus = Innovision.Core.Domain.Enums.AccountStatus;


namespace Innovision.Core.Application.Requests.Players.Queries;

public class PlayerMigrateAccountDto : IMapFrom<Account>
{
    public long AccountInfoId { get; set; }
    public Guid AccountObjectId { get; set; }
    public Guid AccountCreditId { get; set; }
    public Guid AccountBonusId { get; set; }
    public Guid UserId { get; set; }
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
    public string UserType { get; set; }
    public int UserTypeId { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public bool IsMain { get; set; }
    public bool IsActive { get; set; }
    public string AccountStatus { get; set; }
    public int AccountStatusId { get; set; }
    public int? SalaryRange { get; set; }
    public string RefferralCode { get; set; }
    public string ValidId { get; set; }
    public string FrontIdPath { get; set; }
    public string BackIdPath { get; set; }
    public string SignaturePath { get; set; }
    public string ProfilePath { get; set; }
    public string SelfiePath { get; set; }
    public bool IsVerified { get; set; }
    public string Remarks { get; set; }
    public string PaymentAccountId { get; set; }
    public bool ForVerification { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, PlayerMigrateAccountDto>()
            .ForMember(t => t.UserType, f => f.MapFrom(src => src.UserType.UserTypeName))
            .ForMember(t => t.BranchName, f => f.MapFrom(src => src.Branch.BranchName))
            .ForMember(t => t.AccountStatus, f => f.MapFrom(src => MapAccountStatusToString(src.AccountStatusId)));
    }

    private static string MapAccountStatusToString(int accountStatusId)
    {
        return accountStatusId switch
        {
            AccStatus.ForApproval => "For Approval",
            AccStatus.Approved => "Approved",
            AccStatus.Declined => "Declined",
            AccStatus.Block => "Blocked",
            AccStatus.Migrated => "Migrated",
            AccStatus.Deleted => "Deleted",
            AccStatus.Completed => "Completed",
            _ => "Unknown"
        };
    }
}
