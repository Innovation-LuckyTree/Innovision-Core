using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Requests.Orders.Queries;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries;

public class JackpotWinnerDto : IMapFrom<JackpotWinner>
{
    public long JackpotWinnerId { get; set; }
    public long AccountInfoId { get; set; }
    public int CompanyGameId { get; set; }
    public string TransactionNo { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BetValue { get; set; }
    public long DrawResultId { get; set; }
    public string GameTypeName { get; set; }
    public int GameId { get; set; }
    public string DrawResult { get; set; }
    public long BetTransactionId { get; set; }
    public long GameScheduleId { get; set; }
    public  DateTimeOffset DrawDate { get; set; }
    public TimeSpan DrawTime { get; set; }
    public decimal PrizePoolAmount { get; set; }
    public decimal NetWinAmount { get; set; }
    public decimal GrossWinAmount { get; set; }
    public int NumberOfWinners { get; set; }
    public decimal TotalBetAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TaxPercentage { get; set; }
    public long? ApproverAccountId { get; set; }
    public long? ReleaserAccountId { get; set; }
    public int JackpotWinnerStatusId { get; set; }
    public string JackpotWinnerStatus { get; set; }
    public string Remarks { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public IEnumerable<JackpotWinnerAttachmentDto> JackpotWinnerAttachments { get; set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<JackpotWinner, JackpotWinnerDto>()
            .ForMember(t => t.JackpotWinnerId, f => f.MapFrom(src => src.JackpotWinnerId))
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.UserId, f => f.MapFrom(src => src.Account.UserId))
            .ForMember(t => t.CompanyGameId, f => f.MapFrom(src => src.CompanyGameId))
            .ForMember(t => t.TransactionNo, f => f.MapFrom(src => src.TransactionNo))
            .ForMember(t => t.FirstName, f => f.MapFrom(src => src.Account.FirstName))
            .ForMember(t => t.LastName, f => f.MapFrom(src => src.Account.LastName))
            .ForMember(t => t.BetValue, f => f.MapFrom(src => src.BetValue))
            .ForMember(t => t.DrawResultId, f => f.MapFrom(src => src.DrawResultId))
            .ForMember(t => t.GameTypeName, f => f.MapFrom(src => src.GameTypeName))
            .ForMember(t => t.GameId, f => f.MapFrom(src => src.GameId))
            .ForMember(t => t.DrawResult, f => f.MapFrom(src => src.DrawResult))
            .ForMember(t => t.BetTransactionId, f => f.MapFrom(src => src.BetTransactionId))
            .ForMember(t => t.GameScheduleId, f => f.MapFrom(src => src.GameScheduleId))
            .ForMember(t => t.DrawDate, f => f.MapFrom(src => src.DrawDate))
            .ForMember(t => t.DrawTime, f => f.MapFrom(src => src.DrawTime))
            .ForMember(t => t.PrizePoolAmount, f => f.MapFrom(src => src.PrizePoolAmount))
            .ForMember(t => t.NetWinAmount, f => f.MapFrom(src => src.NetWinAmount))
            .ForMember(t => t.GrossWinAmount, f => f.MapFrom(src => src.GrossWinAmount))
            .ForMember(t => t.NumberOfWinners, f => f.MapFrom(src => src.NumberOfWinners))
            .ForMember(t => t.TotalBetAmount, f => f.MapFrom(src => src.TotalBetAmount))
            .ForMember(t => t.TaxAmount, f => f.MapFrom(src => src.TaxAmount))
            .ForMember(t => t.TaxPercentage, f => f.MapFrom(src => src.TaxPercentage))
            .ForMember(t => t.ApproverAccountId, f => f.MapFrom(src => src.ApproverAccountId))
            .ForMember(t => t.ReleaserAccountId, f => f.MapFrom(src => src.ReleaserAccountId))
            .ForMember(t => t.JackpotWinnerStatusId, f => f.MapFrom(src => src.JackpotWinnerStatusId))
            .ForMember(t => t.JackpotWinnerStatus, f => f.MapFrom(src => src.JackpotWinnerStatus.Name))
            .ForMember(t => t.Remarks, f => f.MapFrom(src => src.Remarks))
            .ForMember(t => t.BranchId, f => f.MapFrom(src => src.Account.BranchId))
            .ForMember(t => t.BranchName, f => f.MapFrom(src => src.Account.Branch.BranchName))
            .ForMember(t => t.JackpotWinnerAttachments, f => f.MapFrom(src => src.JackpotWinnerAttachments));
    }
}