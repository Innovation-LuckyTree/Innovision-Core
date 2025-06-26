using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsList;

public class OrderItemDetailDto : IMapFrom<OrderItem>
{
    public long OrderItemId { get; set; }
    public long OrderId { get; set; }
    public long AccountInfoId { get; set; }
    public bool Used { get; set; } = false;
    public string Values { get; set; }
    public decimal AmountBet { get; set; } = 0;
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public int GameId { get; set; }
    public string GameName { get; set; }
    public int GameTypeId { get; set; }
    public string GameTypeName { get; set; }
    public bool FloatingBet { get; set; }
    public bool IsBonus { get; set; }
    public  DateTimeOffset TransactionDate { get; set; }
    public  DateTimeOffset? UsedDate { get; set; }    

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderItem, OrderItemDetailDto>()
            .ForMember(t => t.OrderItemId, f => f.MapFrom(src => src.OrderItemId))
            .ForMember(t => t.OrderId, f => f.MapFrom(src => src.OrderId))
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.Used, f => f.MapFrom(src => src.Used))
            .ForMember(t => t.Values, f => f.MapFrom(src => src.Values))
            .ForMember(t => t.AmountBet, f => f.MapFrom(src => src.AmountBet))
            .ForMember(t => t.UserId, f => f.MapFrom(src => src.AccountInfo.UserId))
            .ForMember(t => t.FirstName, f => f.MapFrom(src => src.AccountInfo.FirstName))
            .ForMember(t => t.LastName, f => f.MapFrom(src => src.AccountInfo.LastName))
            .ForMember(t => t.BranchId, f => f.MapFrom(src => src.AccountInfo.BranchId))
            .ForMember(t => t.BranchName, f => f.MapFrom(src => src.AccountInfo.Branch.BranchName))
            .ForMember(t => t.BranchCode, f => f.MapFrom(src => src.AccountInfo.Branch.BranchCode))
            // .ForMember(t => t.GameId, f => f.MapFrom(src => src.GameType.GameId))
            // .ForMember(t => t.GameName, f => f.MapFrom(src => src.GameType.Game.Name))
            // .ForMember(t => t.GameTypeId, f => f.MapFrom(src => src.GameTypeId))
            // .ForMember(t => t.GameTypeName, f => f.MapFrom(src => src.GameType.GameTypeName))
            .ForMember(t => t.FloatingBet, f => f.MapFrom(src => src.Used))
            .ForMember(t => t.UsedDate, f => f.MapFrom(src => src.UsedDate))
            .ForMember(t => t.IsBonus, f => f.MapFrom(src => src.IsBonus))
            .ForMember(t => t.TransactionDate, f => f.MapFrom(src => src.CreatedOn));
    }
}