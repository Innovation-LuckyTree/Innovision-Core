using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.JackpotWinners.Queries;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.JackpotWinners.Commands.AddJackpotWinner;

public class AddJackpotWinnerCommandHandler : IRequestHandler<AddJackpotWinnerCommand, JackpotWinnerDto>
{
    private readonly ICoreDbContext _coreDbContext;
    private readonly IMapper _mapper;

    public AddJackpotWinnerCommandHandler(ICoreDbContext coreDbContext, IMapper mapper)
    {
        _coreDbContext = coreDbContext;
        _mapper = mapper;
    }

    public async Task<JackpotWinnerDto> Handle(AddJackpotWinnerCommand request, CancellationToken cancellationToken)
    {
        var existingJackpotWinner = await _coreDbContext.JackpotWinners
            .Where(o => o.GameScheduleId == request.GameScheduleId && o.BetTransactionId == request.BetTransactionId)
            .ProjectTo<JackpotWinnerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingJackpotWinner != null)
            return existingJackpotWinner;

        JackpotWinner jackpotWinner = new()
        {
            AccountInfoId = request.AccountInfoId,
            CompanyGameId = request.CompanyGameId,
            TransactionNo = request.TransactionNo,
            BetValue = request.BetValue,
            DrawResultId = request.DrawResultId,
            GameTypeName = request.GameTypeName,
            GameId = request.GameId,
            DrawResult = request.DrawResult,
            BetTransactionId = request.BetTransactionId,
            GameScheduleId = request.GameScheduleId,
            DrawDate = request.DrawDate,
            DrawTime = request.DrawTime,
            PrizePoolAmount = request.PrizePoolAmount,
            NetWinAmount = request.NetWinAmount,
            GrossWinAmount = request.GrossWinAmount,
            NumberOfWinners = request.NumberOfWinners,
            TotalBetAmount = request.TotalBetAmount,
            TaxAmount = request.TaxAmount,
            TaxPercentage = request.TaxPercentage,
            ApproverAccountId = request.ApproverAccountId,
            ReleaserAccountId = request.ReleaserAccountId,
            JackpotWinnerStatusId = request.JackpotWinnerStatusId
        };

        _coreDbContext.JackpotWinners.Add(jackpotWinner);

        try
        {
            await _coreDbContext.SaveChangesAsync(cancellationToken);
        }
        catch(Exception ex)
        {

        }

        return _mapper.Map<JackpotWinnerDto>(jackpotWinner);
    }
}