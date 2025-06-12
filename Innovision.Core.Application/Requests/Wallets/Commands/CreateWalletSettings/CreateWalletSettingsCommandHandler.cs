using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Wallets.Queries;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Wallets.Commands.CreateWalletSettings;

public class CreateWalletSettingsCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator) : IRequestHandler<CreateWalletSettingsCommand, WalletSettingDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    public async Task<WalletSettingDto> Handle(CreateWalletSettingsCommand request, CancellationToken cancellationToken)
    {
        var companySettingsExist = await _coreDbContext.WalletSettings
            .ProjectTo<WalletSettingDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (companySettingsExist != null)
            return companySettingsExist;

        var walletSettings = new WalletSetting
        {
            InitialMinimumDeposit = request.InitialMinimumDeposit,
            SubsequentMinimumDeposit = request.SubsequentMinimumDeposit,
            MaximumDepositAtOnce = request.MaximumDepositAtOnce,
            MaximumDepositPerDay = request.MaximumDepositPerDay,
            InitialMinimumWithdraw = request.InitialMinimumWithdraw,
            SubsequentMinimumWithdraw = request.SubsequentMinimumWithdraw,
            MaximumWithdrawAtOnce = request.MaximumWithdrawAtOnce,
            MaximumWithdrawPerDay = request.MaximumWithdrawPerDay
        };

        _coreDbContext.WalletSettings.Add(walletSettings);

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<WalletSettingDto>(walletSettings);
    }
}
