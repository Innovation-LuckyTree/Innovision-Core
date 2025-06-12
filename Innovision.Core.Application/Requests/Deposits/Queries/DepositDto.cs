using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Deposits.Queries;

public class DepositDto : IMapFrom<Deposit>
{
    public long DepositId { get; set; }
    public long AccountInfoId { get; set; }
    public string TransactionNo { get; set; }
    public decimal Amount { get; set; }
    public int Status { get; set; }
    public string DepositStatus { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PaymentMethodId { get; set; }
    public string PaymentMethod { get; set; }
    public  DateTimeOffset DateCreated { get; set; }
    public  DateTimeOffset? TransactionDate { get; set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Deposit, DepositDto>()
            .ForMember(t => t.DepositId, f => f.MapFrom(src => src.DepositId))
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.TransactionNo, f => f.MapFrom(src => src.TransactionNo))
            .ForMember(t => t.Amount, f => f.MapFrom(src => src.Amount))
            .ForMember(t => t.Status, f => f.MapFrom(src => src.DepositStatusId))
            .ForMember(t => t.FirstName, f => f.MapFrom(src => src.AccountInfo.FirstName))
            .ForMember(t => t.LastName, f => f.MapFrom(src => src.AccountInfo.LastName))
            .ForMember(t => t.PaymentMethodId, f => f.MapFrom(src => src.PaymentMethodId))
            .ForMember(t => t.PaymentMethod, f => f.MapFrom(src => src.PaymentMethod.Name))
            .ForMember(t => t.DateCreated, f => f.MapFrom(src => src.CreatedOn))
            .ForMember(t => t.TransactionDate, f => f.MapFrom(src => src.TransactionDate))
            .ForMember(t => t.DepositStatus, f => f.MapFrom(src => src.DepositStatus.Name));
    }

}