using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.BetTransactions.Queries;

public class BetTransactionDto : IMapFrom<BetTransaction>
{
    public long BetTransactionId { get; set; }
    public long AccountInfoId { get; set; }
    public long ReferenceId { get; set; }
    public long? DrawResultId { get; set; }
    public string RoundReference { get; set; }
    public int GameId { get; set; }
    public string BetValue { get; set; }
    public Guid? ItemId { get; set; }
    public string TransactionType { get; set; }
    public decimal AmountBet { get; set; } = 0;
    public bool IsBonus { get; set; } = false;
    public decimal WinAmount { get; set; } = 0;
    public bool VoidTransaction { get; set; } = false;
    public DateTime? VoidTransactionDate { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<BetTransaction, BetTransactionDto>()
            .ForMember(t => t.BetTransactionId, f => f.MapFrom(src => src.BetTransactionId))
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.ReferenceId, f => f.MapFrom(src => src.ReferenceId))
            .ForMember(t => t.DrawResultId, f => f.MapFrom(src => src.DrawResultId))
            .ForMember(t => t.RoundReference, f => f.MapFrom(src => src.RoundReference))
            .ForMember(t => t.GameId, f => f.MapFrom(src => src.GameId))
            .ForMember(t => t.BetValue, f => f.MapFrom(src => src.BetValue))
            .ForMember(t => t.ItemId, f => f.MapFrom(src => src.ItemId))
            .ForMember(t => t.TransactionType, f => f.MapFrom(src => src.TransactionType))
            .ForMember(t => t.AmountBet, f => f.MapFrom(src => src.AmountBet))
            .ForMember(t => t.IsBonus, f => f.MapFrom(src => src.IsBonus))
            .ForMember(t => t.WinAmount, f => f.MapFrom(src => src.WinAmount))
            .ForMember(t => t.VoidTransaction, f => f.MapFrom(src => src.VoidTransaction))
            .ForMember(t => t.VoidTransactionDate, f => f.MapFrom(src => src.VoidTransactionDate));
    }
}
