using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Orders.Queries;

public class OrderItemDto : IMapFrom<OrderItem>
{
    public long OrderItemId { get; set; }
    public bool Used { get; set; }
    public string TransactionNo { get; set; }
    public string Values { get; set; }
    public int GameTypeId { get; set; }
    public int BetItemType { get; set; }
    public int GameReferenceId { get; set; }
    public string GameType { get; set; }
    public decimal AmountBet { get; set; }
    public  DateTimeOffset? UsedDate { get; set; }
    public decimal ExcessAmount { get; set; } = 0;
    public bool HasExcessAmount { get; set; } = false;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderItem, OrderItemDto>()
            .ForMember(t => t.OrderItemId, f => f.MapFrom(src => src.OrderItemId))
            .ForMember(t => t.Used, f => f.MapFrom(src => src.Used))
            .ForMember(t => t.TransactionNo, f => f.MapFrom(src => src.Order.TransactionNo))
            .ForMember(t => t.Values, f => f.MapFrom(src => src.Values))
            .ForMember(t => t.GameTypeId, f => f.MapFrom(src => src.GameTypeId))
            .ForMember(t => t.BetItemType, f => f.MapFrom(src => src.BetItemType))
            .ForMember(t => t.GameReferenceId, f => f.MapFrom(src => src.GameType.GameReferenceId))
            .ForMember(t => t.GameType, f => f.MapFrom(src => src.GameType.GameTypeDesciption))
            .ForMember(t => t.AmountBet, f => f.MapFrom(src => src.AmountBet))
            .ForMember(t => t.UsedDate, f => f.MapFrom(src => src.UsedDate))
            .ForMember(t => t.ExcessAmount, f => f.MapFrom(src => src.ExcessAmount))
            .ForMember(t => t.HasExcessAmount, f => f.MapFrom(src => src.HasExcessAmount));
    }
}