using AutoMapper;
using Innovision.Core.Application.Common.Models;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetAccountInfoByUserId;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using Innovision.Core.Application.Requests.JackpotWinners.Queries;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.JackpotWinners.Commands.AddJackpotWinner;

public class UpdateJackpotWinnerCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator, ICurrentUserService currentUserService) : IRequestHandler<UpdateJackpotWinnerCommand, JackpotWinnerDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<JackpotWinnerDto> Handle(UpdateJackpotWinnerCommand request, CancellationToken cancellationToken)
    {
        var currentAccount = await _mediator.Send(new GetAccountInfoByUserIdQuery(_currentUserService.UserObjId), cancellationToken);

        var jackpotWinner = await _coreDbContext.JackpotWinners.Where(o => o.JackpotWinnerId == request.JackpotWinnerId).FirstOrDefaultAsync(cancellationToken);

        _ = jackpotWinner ?? throw new EntityNotFoundException(typeof(JackpotWinner).Name, request.JackpotWinnerId);

        if ((request.Attachments?.Count() ?? 0) > 0)
        {
            var attachments = GetJackpotWinnerAttachments(jackpotWinner.JackpotWinnerId, request.Attachments);
            _coreDbContext.JackpotWinnerAttachments.AddRange(attachments);
        }

        if (request.TaxPercentage > 0)
        {
            var taxAmount = jackpotWinner.GrossWinAmount * (request.TaxPercentage / 100);

            jackpotWinner.NetWinAmount = jackpotWinner.GrossWinAmount - taxAmount;
            jackpotWinner.TaxPercentage = request.TaxPercentage;
            jackpotWinner.TaxAmount = taxAmount;
        }

        jackpotWinner.JackpotWinnerStatusId = request.JackpotWinnerStatusId;
        jackpotWinner.ApproverAccountId = currentAccount.AccountInfoId;
        jackpotWinner.LastModified = DateTime.UtcNow;

        _coreDbContext.JackpotWinners.Update(jackpotWinner);
        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<JackpotWinnerDto>(jackpotWinner);
    }

    public IEnumerable<JackpotWinnerAttachment> GetJackpotWinnerAttachments(long jackpotWinnerId, IEnumerable<AttachmentRequest> attachments)
    {
        return attachments.Select(o => new JackpotWinnerAttachment
        {
            JackpotWinnerId = jackpotWinnerId,
            FileName = o.FileName,
            FilePath = o.FilePath,
            FileType = o.FileType
        });
    }
}