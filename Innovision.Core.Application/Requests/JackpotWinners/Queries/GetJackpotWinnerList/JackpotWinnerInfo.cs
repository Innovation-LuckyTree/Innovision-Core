using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerList;

public class JackpotWinnerInfo : IMapFrom<JackpotWinner>
{
    public string TransactionNumber { get; set; }
    public long TransactionId { get; set; }
    public string BetTransactionId { get; set; }
    public long JackpotWinnerId { get; set; }
    public long AccountInfoId { get; set; }
    public decimal WinAmount { get; set; }
    public int JackpotWinnerStatusId { get; set; }
    public string JackpotWinnerStatus { get; set; }
    public string DisplayName { get; set; }
    public  DateTimeOffset DrawDate { get; set; }
    public TimeSpan DrawTime { get; set; }

    public string DrawDateDisplay
    {
        get => $"{DrawDate:MMMM dd, yyyy}";
    }

    public string DrawTimeDisplay
    {
        get => $"{DrawDate + DrawTime:h:mm tt}".ToUpper();
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<JackpotWinner, JackpotWinnerInfo>()
            .ForMember(t => t.TransactionNumber, f => f.MapFrom(src => src.TransactionNo))
            .ForMember(t => t.BetTransactionId, f => f.MapFrom(src => src.BetTransactionId))
            .ForMember(t => t.JackpotWinnerId, f => f.MapFrom(src => src.JackpotWinnerId))
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.WinAmount, f => f.MapFrom(src => src.GrossWinAmount))
            .ForMember(t => t.DrawDate, f => f.MapFrom(src => src.DrawDate))
            .ForMember(t => t.DrawTime, f => f.MapFrom(src => src.DrawTime))
            .ForMember(t => t.JackpotWinnerStatusId, f => f.MapFrom(src => src.JackpotWinnerStatusId))
            .ForMember(t => t.DisplayName, f => f.MapFrom(src => src.Account.FirstName + " " + src.Account.LastName))
            .ForMember(t => t.JackpotWinnerStatus, f => f.MapFrom(src => src.JackpotWinnerStatus.Name))
            .ForMember(t => t.DrawDate, f => f.MapFrom(src => src.DrawDate))
            .ForMember(t => t.DrawTime, f => f.MapFrom(src => src.DrawTime));
    }
}
