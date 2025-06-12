using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Queries
{
  public class BlockUserDto : IMapFrom<BlockedUserHistory>
  {
    public long BlockedUserHistoryId { get; set; }
    public long AccountInfoId { get; set; }
    public  DateTimeOffset BlockedDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public string? Remarks { get; set; }
    public string? UserTypeId { get; set; }
    public string? Fullname { get; set; }
    public string? MobileNumber { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<BlockedUserHistory, BlockUserDto>()
          .ForMember(t => t.BlockedUserHistoryId, f => f.MapFrom(src => src.BlockedUserHistoryId))
          .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
          .ForMember(t => t.BlockedDate, f => f.MapFrom(src => src.BlockedDate))
          .ForMember(t => t.IsActive, f => f.MapFrom(src => src.IsActive))
          .ForMember(t => t.Remarks, f => f.MapFrom(src => src.Remarks))
          .ForMember(t => t.UserTypeId, f => f.MapFrom(src => src.Account.UserTypeId))
          .ForMember(t => t.Fullname, f => f.MapFrom(src => $"{src.Account.FirstName} {src.Account.LastName}"))
          .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.Account.MobileNumber));
    }
  }
}
