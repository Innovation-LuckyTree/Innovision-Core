using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsList;

public class BetTransactionDetailDto : IMapFrom<BetTransaction>
{
    public long BetTransactionId { get; set; }
    public long OrderId { get; set; }
    public long AccountInfoId { get; set; }
    public bool Used { get; set; } = false;
    public string Values { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
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
    public DateTime TransactionDate { get; set; }
    public DateTime? UsedDate { get; set; }    

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BetTransaction, BetTransactionDetailDto>()
            .ForMember(t => t.BetTransactionId, f => f.MapFrom(src => src.BetTransactionId))
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
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
            // .ForMember(t => t.FloatingBet, f => f.MapFrom(src => !src.Used))
            // .ForMember(t => t.UsedDate, f => f.MapFrom(src => src.UsedDate))
            .ForMember(t => t.IsBonus, f => f.MapFrom(src => src.IsBonus))
            .ForMember(t => t.TransactionDate, f => f.MapFrom(src => src.CreatedOn));
    }
}