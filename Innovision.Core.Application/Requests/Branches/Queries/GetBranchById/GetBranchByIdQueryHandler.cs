using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetBranchById;

public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, ApiResponse<BranchDetailsDto>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBranchByIdQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<BranchDetailsDto>> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var branch = await _dbContext.Branches
            .Include(o => o.Account)
            .Where(x => x.BranchId == request.BranchId)
            .ProjectTo<BranchDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return new ApiResponse<BranchDetailsDto>() { Data = branch };
    }
}
