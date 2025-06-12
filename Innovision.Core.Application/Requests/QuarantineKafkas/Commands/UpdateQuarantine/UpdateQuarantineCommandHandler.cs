using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Innovision.Core.Application.Exceptions;
using AutoMapper;
using Innovision.Core.Application.Requests.QuarantineKafkas.Queries;

namespace Innovision.Core.Application.Requests.QuarantineKafkas.Commands.UpdateQuarantine
{
  public class UpdateQuarantineCommandHandler : IRequestHandler<UpdateQuarantineCommand, QuarantineKafkaDto>
  {
    private readonly ICoreDbContext _coreDbContext;
    private readonly IMapper _mapper;

    public UpdateQuarantineCommandHandler(ICoreDbContext coreDbContext, IMapper mapper)
    {
      _coreDbContext = coreDbContext;
      _mapper = mapper;
    }

    public async Task<QuarantineKafkaDto> Handle(UpdateQuarantineCommand request, CancellationToken cancellationToken)
    {
      var quarantine = await _coreDbContext.QuarantineKafkas
          .FirstOrDefaultAsync(o => o.QuarantineKafkaId == request.QuarantineKafkaId, cancellationToken)
          ?? throw new EntityNotFoundException("QuarantineKafka", request.QuarantineKafkaId);

      if (request.KafkaValue != null) quarantine.KafkaValue = request.KafkaValue;
      if (request.Attempts.HasValue) quarantine.Attempts = request.Attempts;
      if (request.ErrorCode != null) quarantine.ErrorCode = request.ErrorCode;
      if (request.ErrorMessage != null) quarantine.ErrorMessage = request.ErrorMessage;
      if (request.Status.HasValue) quarantine.Status = request.Status.Value;
      if (request.AttemptedOn.HasValue) quarantine.AttemptedOn = request.AttemptedOn;
      if (request.CompletedOn.HasValue) quarantine.CompletedOn = request.CompletedOn;

      await _coreDbContext.SaveChangesAsync(cancellationToken);

      return _mapper.Map<QuarantineKafkaDto>(quarantine);
    }
  }
}
