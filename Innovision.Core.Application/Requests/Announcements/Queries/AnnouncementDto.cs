using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Announcements.Queries;

public class AnnouncementDto : IMapFrom<Announcement>
{
    public long AnnouncementId { get; set; }
    public int BranchId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SendTo { get; set; }
    public  DateTimeOffset? StartDate { get; set; }
    public  DateTimeOffset? EndDate { get; set; }
    public bool IsBanner { get; set; }
    public int Status { get; set; }
    public  DateTimeOffset CreatedOn { get; set; }

    public void Mapping(Profile profile)
  {
    profile.CreateMap<Announcement, AnnouncementDto>()
        .ForMember(t => t.AnnouncementId, f => f.MapFrom(src => src.AnnouncementId))
        .ForMember(t => t.BranchId, f => f.MapFrom(src => src.BranchId))
        .ForMember(t => t.Title, f => f.MapFrom(src => src.Title))
        .ForMember(t => t.Description, f => f.MapFrom(src => src.Description))
        .ForMember(t => t.SendTo, f => f.MapFrom(src => src.SendTo))
        .ForMember(t => t.StartDate, f => f.MapFrom(src => src.StartDate))
        .ForMember(t => t.EndDate, f => f.MapFrom(src => src.EndDate))
        .ForMember(t => t.IsBanner, f => f.MapFrom(src => src.IsBanner))
        .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
        .ForMember(t => t.Status, f => f.MapFrom(src => src.Status));
  }
}