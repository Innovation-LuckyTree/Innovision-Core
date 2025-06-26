using AutoMapper;
using Innovision.Core.Application.Common.Constants;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Upload.Application.Common;

namespace Innovision.Core.Application.Requests.Withdrawals;

public class WithdrawalDto : IMapFrom<Withdrawal>
{
    public long TransactionId { get; set; }
    public Guid accountObjectId { get; set; }
    public string TransactionNo { get; set; }
    public long AccountInfoId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public int Status { get; set; }
    public  DateTimeOffset TransactionDate { get; set; }
    public string Remarks { get; set; }
    public string BankName { get; set; }
    public string BankInfo { get; set; }
    public string ImageProof { get; set; }

    public void Mapping(Profile profile)
    {
        Crypto crypto = new();
        profile.CreateMap<Withdrawal, WithdrawalDto>()
            .ForMember(t => t.TransactionId, f => f.MapFrom(src => src.TransactionId))
            .ForMember(t => t.accountObjectId, f => f.MapFrom(src => src.AccountInfo.AccountObjectId))
            .ForMember(t => t.TransactionNo, f => f.MapFrom(src => src.TransactionNo))
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.Name, f => f.MapFrom(src => src.AccountInfo.FirstName + " " + src.AccountInfo.LastName))
            .ForMember(t => t.PaymentMethod, f => f.MapFrom(src => src.PaymentMethod))
            .ForMember(t => t.Remarks, f => f.MapFrom(src => src.Remarks))
            .ForMember(t => t.Status, f => f.MapFrom(src => src.Status))
            .ForMember(t => t.BankInfo, f => f.MapFrom(src => (src.BankInfo != null) ? crypto.Decrypt(src.BankInfo) : null))
            .ForMember(t => t.ImageProof, f => f.MapFrom(src => src.ImageProof))
            .ForMember(t => t.BankName, f => f.MapFrom(src => src.BankReference.Name))
            .ForMember(t => t.TransactionDate, f => f.MapFrom(src => src.TransactionDate));
    }

    public string StatusDisplay
    {
        get
        {
            string withdrawStatus = string.Empty;
            switch (Status)
            {
                case 0:
                    withdrawStatus = WalletWithdrawalStatusString.Pending;
                    break;
                case 1:
                    withdrawStatus = WalletWithdrawalStatusString.Complete;
                    break;
                case 2:
                    withdrawStatus = WalletWithdrawalStatusString.Declined;
                    break;
                case 3:
                    withdrawStatus = WalletWithdrawalStatusString.Void;
                    break;
                default:
                    withdrawStatus = WalletWithdrawalStatusString.Failed;
                    break;
            }
            return withdrawStatus;
        }
    }
}
