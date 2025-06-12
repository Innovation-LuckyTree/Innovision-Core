using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.ApplicationVersions;

public class SelfLimitDto : IMapFrom<SelfLimit>
{
    public int SelfLimitId { get; set; }
    public long AccountId { get; set; }
    public decimal AmountLimit { get; set; }
    public int Status { get; set; } = 1; //1 - for active
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public TimeSpan TimeDuration { get; set; }
    public  DateTimeOffset CreatedOn { get; set; }

    public string FullName
    {
        get => $"{FirstName} {LastName}";
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SelfLimit, SelfLimitDto>()
            .ForMember(t => t.SelfLimitId, f => f.MapFrom(src => src.SelfLimitId))
            .ForMember(t => t.AccountId, f => f.MapFrom(src => src.AccountId))
            .ForMember(t => t.FirstName, f => f.MapFrom(src => src.Account.FirstName))
            .ForMember(t => t.LastName, f => f.MapFrom(src => src.Account.LastName))
            .ForMember(t => t.AmountLimit, f => f.MapFrom(src => src.AmountLimit))
            .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
            .ForMember(t => t.Status, f => f.MapFrom(src => src.Status));
    }
}

