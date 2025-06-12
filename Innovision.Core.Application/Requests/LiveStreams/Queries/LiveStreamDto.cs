using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.LiveStreams.Queries;

public class LiveStreamDto : IMapFrom<LiveStream>
{
    public int LiveStreamId { get; set; }
    public string Title { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public  DateTimeOffset DateCreated { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LiveStream, LiveStreamDto>()
            .ForMember(t => t.LiveStreamId, f => f.MapFrom(src => src.LiveStreamId))
            .ForMember(t => t.Title, f => f.MapFrom(src => src.Title)) 
            .ForMember(t => t.Link, f => f.MapFrom(src => src.Link)) 
            .ForMember(t => t.Description, f => f.MapFrom(src => src.Description)) 
            .ForMember(t => t.DateCreated, f => f.MapFrom(src => src.CreatedOn));
    }
}