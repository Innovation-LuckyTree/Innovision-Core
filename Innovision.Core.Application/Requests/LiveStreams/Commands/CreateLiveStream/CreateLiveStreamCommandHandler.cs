using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.LiveStreams.Queries;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.LiveStreams.Commands.CreateLiveStream;

public class CreateLiveStreamCommandHandler : IRequestHandler<CreateLiveStreamCommand, LiveStreamDto>
{
    private readonly ICoreDbContext _coreDbContext;
    private readonly IMapper _mapper;

    public CreateLiveStreamCommandHandler(IMapper mapper, ICoreDbContext coreDbContext)
    {
        _mapper = mapper;
        _coreDbContext = coreDbContext;
    }

    public async Task<LiveStreamDto> Handle(CreateLiveStreamCommand request, CancellationToken cancellationToken)
    {
        LiveStream liveStream = new()
        {
            Title = request.Title,
            Link = request.Link,
            Description = request.Description
        };

        _coreDbContext.LiveStreams.Add(liveStream);

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<LiveStreamDto>(liveStream);
    }
}