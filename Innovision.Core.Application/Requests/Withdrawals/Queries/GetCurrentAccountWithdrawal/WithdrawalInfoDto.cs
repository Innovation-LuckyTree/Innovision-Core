using AutoMapper;
using Innovision.Core.Application.Common.Constants;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Withdrawals.GetCurrentAccountWithdrawal;

public class WithdrawalInfoDto : IMapFrom<Withdrawal>
{
    public long TransactionId { get; set; }
    public string TransactionNo { get; set; }
    public long AccountInfoId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public int Status { get; set; }
    public  DateTimeOffset TransactionDate { get; set; }
    public string Remarks { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Withdrawal, WithdrawalInfoDto>()
            .ForMember(t => t.TransactionId, f => f.MapFrom(src => src.TransactionId))
            .ForMember(t => t.TransactionNo, f => f.MapFrom(src => src.TransactionNo))
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.Name, f => f.MapFrom(src => src.AccountInfo.FirstName + " " + src.AccountInfo.LastName))
            .ForMember(t => t.PaymentMethod, f => f.MapFrom(src => src.PaymentMethod))
            .ForMember(t => t.Remarks, f => f.MapFrom(src => src.Remarks))
            .ForMember(t => t.Status, f => f.MapFrom(src => src.Status))
            .ForMember(t => t.TransactionDate, f => f.MapFrom(src => src.TransactionDate.Date))
            .ForMember(t => t.BranchId, f => f.MapFrom(src => src.AccountInfo.BranchId))
            .ForMember(t => t.BranchName, f => f.MapFrom(src => src.AccountInfo.Branch.BranchName))
            .ForMember(t => t.TransactionDate, f => f.MapFrom(src => src.TransactionDate.Date));
    }

    public string StatusDisplay
    {
        get
        {
            return Status switch
            {
                0 => WalletWithdrawalStatusString.Pending,
                1 => WalletWithdrawalStatusString.Complete,
                2 => WalletWithdrawalStatusString.Declined,
                3 => WalletWithdrawalStatusString.Void,
                4 => WalletWithdrawalStatusString.Failed,
                _ => ""
            };
        }
    }
}
