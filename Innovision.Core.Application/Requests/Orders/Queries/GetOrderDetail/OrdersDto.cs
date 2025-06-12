using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderDetail;

public class OrdersDto : IMapFrom<Order>
{
    public long OrderId { get; set; }
    public long PlayerAccountId { get; set; }
    public int GameId { get; set; }
    public string GameName { get; set; }
    public string TransactionNo { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalNoOfItems { get; set; }
    public  DateTimeOffset DateOfTransaction { get; set; }
    public bool IsBonus { get; set; }
    public bool IsDeleted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrdersDto>()
            .ForMember(t => t.OrderId, f => f.MapFrom(src => src.OrderId))
            .ForMember(t => t.PlayerAccountId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.GameId, f => f.MapFrom(src => src.GameId))
            .ForMember(t => t.TransactionNo, f => f.MapFrom(src => src.TransactionNo))
            .ForMember(t => t.TotalAmount, f => f.MapFrom(src => src.TotalAmount))
            .ForMember(t => t.TotalNoOfItems, f => f.MapFrom(src => src.TotalNoOfItems))
            .ForMember(t => t.DateOfTransaction, f => f.MapFrom(src => src.CreatedOn))
            .ForMember(t => t.IsBonus, f => f.MapFrom(src => src.IsBonus))
            .ForMember(t => t.IsDeleted, f => f.MapFrom(src => src.IsDeleted));
    }
}
