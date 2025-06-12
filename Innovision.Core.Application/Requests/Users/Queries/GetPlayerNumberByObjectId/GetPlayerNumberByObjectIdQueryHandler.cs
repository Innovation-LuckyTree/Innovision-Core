using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetPlayerByObjectId;

public class GetPlayerNumberByObjectIdQueryHandler : IRequestHandler<GetPlayerNumberByObjectIdQuery, ApiResponse<string>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPlayerNumberByObjectIdQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<string>> Handle(GetPlayerNumberByObjectIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _dbContext.Accounts
                .Where(m => m.AccountObjectId == request.AccountObjctId).AsQueryable();

            var userInfo = await query
                .ProjectTo<SystemUser>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefaultAsync(cancellationToken);


            return new ApiResponse<string>() { Data = userInfo!.MobileNumber };
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}
