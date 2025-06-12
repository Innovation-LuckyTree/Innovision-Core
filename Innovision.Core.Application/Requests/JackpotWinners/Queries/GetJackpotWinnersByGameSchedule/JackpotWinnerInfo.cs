using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnersByGame;

public class JackpotWinnerInfo : IMapFrom<JackpotWinner>
{
    public long JackpotWinnerId { get; set; }
    public string TransactionNo { get; set; }
    public decimal GrossWinAmount { get; set; }
    public int JackpotWinnerStatusId { get; set; }
    public string JackpotWinnerStatus { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<JackpotWinner, JackpotWinnerInfo>()
            .ForMember(t => t.JackpotWinnerId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.TransactionNo, f => f.MapFrom(src => src.TransactionNo))
            .ForMember(t => t.GrossWinAmount, f => f.MapFrom(src => src.GrossWinAmount))
            .ForMember(t => t.JackpotWinnerStatusId, f => f.MapFrom(src => src.JackpotWinnerStatusId))
            .ForMember(t => t.JackpotWinnerStatus, f => f.MapFrom(src => src.JackpotWinnerStatus.Name));
    }
}
