using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;

public class AccountInfoDto : IMapFrom<Account>
{
    public long AccountInfoId { get; set; }
    public Guid AccountObjectId { get; set; }
    public Guid AccountCreditId { get; set; }
    public Guid? AccountBonusId { get; set; }
    public Guid UserId { get; set; }
    public string PaymentAccount { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public string ProfilePath { get; set; }
    public int? BranchId { get; set; }
    

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, AccountInfoDto>()
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
            .ForMember(t => t.AccountCreditId, f => f.MapFrom(src => src.AccountCreditId))
            .ForMember(t => t.AccountBonusId, f => f.MapFrom(src => src.AccountBonusId))
            .ForMember(t => t.UserId, f => f.MapFrom(src => src.UserId))
            .ForMember(t => t.FirstName, f => f.MapFrom(src => src.FirstName))
            .ForMember(t => t.LastName, f => f.MapFrom(src => src.LastName))
            .ForMember(t => t.MiddleName, f => f.MapFrom(src => src.MiddleName))
            .ForMember(t => t.Email, f => f.MapFrom(src => src.Email))
            .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
            .ForMember(t => t.ProfilePath, f => f.MapFrom(src => src.ProfilePath))
            .ForMember(t => t.PaymentAccount, f => f.MapFrom(src => src.PaymentAccountId))
            .ForMember(t => t.BranchId, f => f.MapFrom(src => src.BranchId));
    }
}