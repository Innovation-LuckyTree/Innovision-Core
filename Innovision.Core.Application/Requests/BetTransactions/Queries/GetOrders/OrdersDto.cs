using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrders;

public class OrdersDto : IMapFrom<BetTransaction>
{
    public long OrderId { get; set; }
    public int GameId { get; set; }
    public string TransactionNo { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalNoOfItems { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public bool IsBonus { get; set; }
    public bool IsDeleted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BetTransaction, OrdersDto>()
            // .ForMember(t => t.OrderId, f => f.MapFrom(src => src.OrderId))
            .ForMember(t => t.GameId, f => f.MapFrom(src => src.GameId))
            // .ForMember(t => t.TransactionNo, f => f.MapFrom(src => src.TransactionNo))
            // .ForMember(t => t.TotalAmount, f => f.MapFrom(src => src.TotalAmount))
            // .ForMember(t => t.TotalNoOfItems, f => f.MapFrom(src => src.TotalNoOfItems))
            .ForMember(t => t.DateOfTransaction, f => f.MapFrom(src => src.CreatedOn))
            .ForMember(t => t.IsBonus, f => f.MapFrom(src => src.IsBonus));
            // .ForMember(t => t.IsDeleted, f => f.MapFrom(src => src.IsDeleted));
    }
}
