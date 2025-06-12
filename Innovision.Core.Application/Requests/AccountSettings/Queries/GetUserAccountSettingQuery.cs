using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.AccountSettings.Queries
{
    public class GetUserAccountSettingQuery : IRequest<ApiResponse<AccountSetting>>;

    public class GetUserAccountSettingQueryHandler : IRequestHandler<GetUserAccountSettingQuery, ApiResponse<AccountSetting>>
    {
        private readonly IMapper _mapper;
        private readonly ICoreDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public GetUserAccountSettingQueryHandler(IMapper mapper, ICoreDbContext dbContext, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<AccountSetting>> Handle(GetUserAccountSettingQuery request, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts
                .Include(m => m.AccountSetting)
                .Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefaultAsync(cancellationToken);

            var accountSetting = account.AccountSetting;
            AccountSetting acc = new AccountSetting();

            if (accountSetting != null)
            {
                acc.AccountSettingId = accountSetting.AccountSettingId;
                acc.SmsNotification = accountSetting.SmsNotification;
                acc.InAppNotification = accountSetting.InAppNotification;
                acc.EmailNotification = accountSetting.EmailNotification;
            }

            return new ApiResponse<AccountSetting>() { Data = acc  };
        }
    }
}
