using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Wallets.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Wallets.Commands.UpdateWithdrawalSettings;

public class UpdateWithdrawalSettingsCommandHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<UpdateWithdrawalSettingsCommand, WalletSettingDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<WalletSettingDto> Handle(UpdateWithdrawalSettingsCommand request, CancellationToken cancellationToken)
    {
        var walletSettings = await _coreDbContext.WalletSettings
            .Where(o => o.WalletSettingId == request.WalletSettingId)
            .FirstOrDefaultAsync(cancellationToken);

        walletSettings.InitialMinimumWithdraw = request.InitialMinimumWithdraw;
        walletSettings.SubsequentMinimumWithdraw = request.SubsequentMinimumWithdraw;
        walletSettings.MaximumWithdrawAtOnce = request.MaximumWithdrawAtOnce;
        walletSettings.MaximumWithdrawPerDay = request.MaximumWithdrawPerDay;

        _coreDbContext.WalletSettings.Update(walletSettings);

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<WalletSettingDto>(walletSettings);
    }
}