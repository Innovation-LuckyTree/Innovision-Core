using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;


public class SelfExclusionDto : IMapFrom<SelfExclusion>
{
    public int SelfExclusionId { get; set; }
    public long AccountId { get; set; }
    public bool IsIndefinite { get; set; } = false;
    public  DateTimeOffset? DateStart { get; set; }
    public  DateTimeOffset? DateEnd { get; set; }
    public int? Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SelfExclusion, SelfExclusionDto>()
            .ForMember(t => t.SelfExclusionId, f => f.MapFrom(src => src.SelfExclusionId))
            .ForMember(t => t.AccountId, f => f.MapFrom(src => src.AccountId))
            .ForMember(t => t.IsIndefinite, f => f.MapFrom(src => src.IsIndefinite))
            .ForMember(t => t.DateStart, f => f.MapFrom(src => src.DateStart))
            .ForMember(t => t.DateEnd, f => f.MapFrom(src => src.DateEnd))
            .ForMember(t => t.Status, f => f.MapFrom(src => src.Status));
    }
}
