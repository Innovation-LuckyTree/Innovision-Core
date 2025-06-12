using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Accounts.Queries;

public class AccountDto : IMapFrom<Account>
{
    public long AccountInfoId { get; set; }
    public Guid AccountObjectId { get; set; }
    public Guid AccountCreditId { get; set; }
    public string MobileNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, AccountDto>()
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.AccountCreditId, f => f.MapFrom(src => src.AccountCreditId))
            .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
            .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber));
    }
}