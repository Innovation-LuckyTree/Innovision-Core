using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetDownlineAccountIds;

public class DownlineAccountInfo : IMapFrom<Account>
{
    public long AccountInfoId { get; set; }
    public string ReferralKey { get; set; }
    public int UserTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, DownlineAccountInfo>()
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.ReferralKey, f => f.MapFrom(src => src.RefferralKey))
            .ForMember(t => t.UserTypeId, f => f.MapFrom(src => src.UserTypeId));
    }
}
