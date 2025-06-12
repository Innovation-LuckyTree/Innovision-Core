using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Wallets.Queries;

public class WalletSettingDto : IMapFrom<WalletSetting>
{
    public int WalletSettingId { get; set; }
    public decimal InitialMinimumDeposit { get; set; }
    public decimal SubsequentMinimumDeposit { get; set; }
    public decimal MaximumDepositAtOnce { get; set; }
    public decimal MaximumDepositPerDay { get; set; }
    public decimal InitialMinimumWithdraw { get; set; }
    public decimal SubsequentMinimumWithdraw { get; set; }
    public decimal MaximumWithdrawAtOnce { get; set; }
    public decimal MaximumWithdrawPerDay { get; set; }
    public int TaxPercentage { get; set; }
    public decimal TaxableAmount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WalletSetting, WalletSettingDto>()
            .ForMember(t => t.WalletSettingId, f => f.MapFrom(src => src.WalletSettingId))
            .ForMember(t => t.InitialMinimumDeposit, f => f.MapFrom(src => src.InitialMinimumDeposit))
            .ForMember(t => t.SubsequentMinimumDeposit, f => f.MapFrom(src => src.SubsequentMinimumDeposit))
            .ForMember(t => t.MaximumDepositAtOnce, f => f.MapFrom(src => src.MaximumDepositAtOnce))
            .ForMember(t => t.MaximumDepositPerDay, f => f.MapFrom(src => src.MaximumDepositPerDay))
            .ForMember(t => t.InitialMinimumWithdraw, f => f.MapFrom(src => src.InitialMinimumWithdraw))
            .ForMember(t => t.SubsequentMinimumWithdraw, f => f.MapFrom(src => src.SubsequentMinimumWithdraw))
            .ForMember(t => t.MaximumWithdrawAtOnce, f => f.MapFrom(src => src.MaximumWithdrawAtOnce))
            .ForMember(t => t.MaximumWithdrawPerDay, f => f.MapFrom(src => src.MaximumWithdrawPerDay))
            .ForMember(t => t.TaxableAmount, f => f.MapFrom(src => src.TaxableAmount))
            .ForMember(t => t.TaxPercentage, f => f.MapFrom(src => src.TaxPercentage));

    }
}
