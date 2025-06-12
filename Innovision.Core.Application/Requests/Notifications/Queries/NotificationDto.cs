using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Notifications.Queries;

public class NotificationDto : IMapFrom<Notification>
{
  public long AccountInfoId { get; set; }
  public long NotificationId { get; set; }
  public int NotificationTypeId { get; set; }
  public bool IsRead { get; set; } = false;
  public string Title { get; set; }
  public string Description { get; set; }
  public string RedirectUrl { get; set; }
  public  DateTimeOffset? TransactionDate { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Notification, NotificationDto>()
        .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
        .ForMember(t => t.NotificationId, f => f.MapFrom(src => src.NotificationId))
        .ForMember(t => t.NotificationTypeId, f => f.MapFrom(src => src.NotificationTypeId))
        .ForMember(t => t.IsRead, f => f.MapFrom(src => src.IsRead))
        .ForMember(t => t.Title, f => f.MapFrom(src => src.Title))
        .ForMember(t => t.Description, f => f.MapFrom(src => src.Description))
        .ForMember(t => t.RedirectUrl, f => f.MapFrom(src => src.RedirectUrl))
        .ForMember(t => t.TransactionDate, f => f.MapFrom(src => src.CreatedOn));
  }
}