using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Wallets.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Wallets.Commands.UpdateDepositSettings;

public class UpdateDepositSettingsCommandHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<UpdateDepositSettingsCommand, WalletSettingDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<WalletSettingDto> Handle(UpdateDepositSettingsCommand request, CancellationToken cancellationToken)
    {
        var walletSettings = await _coreDbContext.WalletSettings
            .Where(o => o.WalletSettingId == request.WalletSettingId)
            .FirstOrDefaultAsync(cancellationToken);

        walletSettings.InitialMinimumDeposit = request.InitialMinimumDeposit;
        walletSettings.SubsequentMinimumDeposit = request.SubsequentMinimumDeposit;
        walletSettings.MaximumDepositAtOnce = request.MaximumDepositAtOnce;
        walletSettings.MaximumDepositPerDay = request.MaximumDepositPerDay;

        _coreDbContext.WalletSettings.Update(walletSettings);

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<WalletSettingDto>(walletSettings);
    }
}