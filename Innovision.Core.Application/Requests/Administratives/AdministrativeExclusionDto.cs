using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.ApplicationVersions;

public class AdministrativeExclusionDto : IMapFrom<AdministrativeExclusion>
{
    public int AdministrativeExclusionId { get; set; }
    public long AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TimeDurationStr { get; set; }
    public TimeSpan TimeDuration { get; set; }
    public int DayDuration { get; set; }
    public int Status { get; set; }
    public string TimeLeft { get; set; }
    public string GameType { get => $"All"; }
    public  DateTimeOffset DateExpiry { get; set; }
    public  DateTimeOffset CreatedOn { get; set; }

    public string FullName
    {
        get => $"{FirstName} {LastName}";
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AdministrativeExclusion, AdministrativeExclusionDto>()
            .ForMember(t => t.AdministrativeExclusionId, f => f.MapFrom(src => src.AdministrativeExclusionId))
            .ForMember(t => t.AccountId, f => f.MapFrom(src => src.AccountId))
            .ForMember(t => t.FirstName, f => f.MapFrom(src => src.Account.FirstName))
            .ForMember(t => t.LastName, f => f.MapFrom(src => src.Account.LastName))
            .ForMember(t => t.TimeDuration, f => f.MapFrom(src => src.TimeDuration))
            .ForMember(t => t.DayDuration, f => f.MapFrom(src => src.DayDuration))
            .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
            .ForMember(t => t.DateExpiry, f => f.MapFrom(src => src.DateExpiry))
            .ForMember(t => t.TimeDurationStr, f => f.MapFrom(src => src.DayDuration + "|" + src.TimeDuration.Hours))
            .ForMember(t => t.TimeLeft, f => f.MapFrom(src => (src.DateExpiry - src.CreatedOn).Days + "|" + (src.DateExpiry - src.CreatedOn).Hours))
            .ForMember(t => t.Status, f => f.MapFrom(src => src.Status));
    }
}

