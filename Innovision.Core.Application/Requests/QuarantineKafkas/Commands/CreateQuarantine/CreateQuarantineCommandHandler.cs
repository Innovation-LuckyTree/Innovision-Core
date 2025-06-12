using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.QuarantineKafkas.Queries;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.QuarantineKafkas.Commands.CreateQuarantine;

public class CreateQuarantineCommandHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<CreateQuarantineCommand, QuarantineKafkaDto>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;
  public async Task<QuarantineKafkaDto> Handle(CreateQuarantineCommand request, CancellationToken cancellationToken)
  {

    QuarantineKafka quarantine = new()
    {
      KafkaValue = request.KafkaValue,
      KafkaTopic = request.KafkaTopic,
      ErrorCode = request.ErrorCode,
      ErrorMessage = request.ErrorMessage,
      Status = 1, // active upon creation
    };

    _coreDbContext.QuarantineKafkas.Add(quarantine);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    return _mapper.Map<QuarantineKafkaDto>(quarantine);
  }
}