using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Models.Responses;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccount
{
    public class GetCurrentAccountQueryHandler : IRequestHandler<GetCurrentAccountQuery, CurrentAccountResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICoreDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public GetCurrentAccountQueryHandler(IMapper mapper, ICoreDbContext dbContext, ICurrentUserService currentUserService, IMediator mediator)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<CurrentAccountResponse> Handle(GetCurrentAccountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Get all user informations
                var userInfo = await _dbContext.Accounts
                    .Include(m => m.Branch)
                    .Where(m => m.UserId == _currentUserService.UserObjId)
                    .ProjectTo<CurrentAccountDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                //// Get list of user menus by company and role
                //var menuList = await _mediator.Send(new GetMenuSecurityGroupQuery(_currentUserService.RoleId, userInfo?.CompanyId), cancellationToken);

                if (userInfo == null)
                    throw new Exception("User not found!");

                return new CurrentAccountResponse
                {
                    Account = userInfo,
                    //AcocuntMenus = menuList
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
