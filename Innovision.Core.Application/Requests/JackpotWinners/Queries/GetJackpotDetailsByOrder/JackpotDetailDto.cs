using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotDetailsByOrder;

public class JackpotDetailDto : IMapFrom<JackpotWinner>
{
    public long JackpotWinnerId { get; set; }
    public long OrderItemId { get; set; }
    public long DrawResultId { get; set; }
    public long AccountInfoId { get; set; }
    public int StatusId { get; set; }
    public string Status { get; set; }
    public  DateTimeOffset? LastModifiedDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<JackpotWinner, JackpotDetailDto>()
            .ForMember(t => t.JackpotWinnerId, f => f.MapFrom(src => src.JackpotWinnerId))
            .ForMember(t => t.OrderItemId, f => f.MapFrom(src => src.OrderItemId))
            .ForMember(t => t.DrawResultId, f => f.MapFrom(src => src.DrawResultId))
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.StatusId, f => f.MapFrom(src => src.JackpotWinnerStatusId))
            .ForMember(t => t.Status, f => f.MapFrom(src => src.JackpotWinnerStatus.Name))
            .ForMember(t => t.LastModifiedDate, f => f.MapFrom(src => src.LastModified));
    }
}
