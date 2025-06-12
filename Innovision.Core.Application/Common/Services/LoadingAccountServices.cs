using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Queries;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Innovision.Core.Application.Common.Services
{
    public class LoadingAccountServices : ILoadingAccountServices
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public LoadingAccountServices(IMapper mapper, ICoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public (List<UserControl>?, List<UserControl>?) GetAccessControl(UserTypeConfig? userTypeConfig)
        {
            if (userTypeConfig != null)
            {
                var jsonRequestCredit = (userTypeConfig.RequestCredit != null) ? JsonConvert.DeserializeObject<List<UserControl>>(userTypeConfig.RequestCredit) : null;
                var jsonSendCredit = (userTypeConfig.CashinDeposit != null) ? JsonConvert.DeserializeObject<List<UserControl>>(userTypeConfig.CashinDeposit) : null;

                return (jsonRequestCredit, jsonSendCredit);
            }

            var dfJsonRCredit = JsonConvert.DeserializeObject<List<UserControl>>("[{\"userTypeId\":5,\"companyLevel\":false}]");
            var dfJsonSCredit = JsonConvert.DeserializeObject<List<UserControl>>("[{\"userTypeId\":5,\"companyLevel\":false}]");

            return new(dfJsonRCredit, dfJsonSCredit);
        }

        public async Task<List<SystemUserDto>> GetUsersList(List<UserControl>? UserControls, int BranchId, bool serviceProvider, bool isBranchUser, int userType, CancellationToken cancellationToken)
        {
            List<SystemUserDto> respList = [];
            List<List<SystemUserDto>> systemUserDtos = [];

            if (UserControls != null)
            {
                for (int i = 0; i < UserControls.Count; i++)
                {
                    var query = _dbContext.Accounts
                    .Include(m => m.Branch)
                    .Where(m => m.UserTypeId == UserControls[i].UserTypeId 
                        && (m.AccountStatusId == AccountStatus.Approved
                        || (m.AccountStatusId == AccountStatus.Migrated)
                        || (m.AccountStatusId == AccountStatus.Completed))
                        && m.IsActive)
                    .AsQueryable();

                    if (isBranchUser && userType != UserTypes.Agent) {
                        query = query.Where(m => m.IsMain);
                    }

                    if (!serviceProvider)
                    {
                        query = query.Where(m => m.Branch.BranchId == BranchId);
                    }

                    var result = await query
                        .ProjectTo<SystemUserDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    systemUserDtos.Add(result);
                }

                respList.AddRange(systemUserDtos.SelectMany(o => o));
            }

            return respList;
        }

        public async Task<SystemUserVm> GetUsersListPaginate(List<UserControl>? UserControls, int branchId, bool serviceProvider, PagedQuery PagedQuery, Guid UserObjId, int? levelType,  CancellationToken cancellationToken)
        {
            List<int> usetTypeIds = new List<int>();

            if (UserControls != null)
            {
                for (int i = 0; i < UserControls.Count; i++)
                {
                    usetTypeIds.Add(UserControls[i].UserTypeId);
                }
            }

            // all players as default settings
            if (usetTypeIds.Count == 0)
                usetTypeIds.Add(5);

            var query = _dbContext.Accounts
                    .Include(m => m.Branch)
                    .Where(m => usetTypeIds.Contains(m.UserTypeId)
                        && m.AccountObjectId != UserObjId
                        && (m.AccountStatusId == AccountStatus.Approved
                        || (m.AccountStatusId == AccountStatus.Migrated)
                        || (m.AccountStatusId == AccountStatus.Completed))
                        && m.IsActive)
                    .OrderByDescending(m => m.CreatedOn)
                    .AsQueryable();

            var totalCount = query.Count();

            if (PagedQuery != null)
                query = QueryFilter(query, PagedQuery);

            var result = await query
                .ProjectTo<SystemUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new SystemUserVm
            {
                Results = result,
                Total = totalCount,
                PageNumber = (PagedQuery != null) ? PagedQuery.PageNumber : 1,
                PageSize = (PagedQuery != null) ? PagedQuery.PageSize : result.Count()
            };
        }

        private IQueryable<Account> QueryFilter(IQueryable<Account> query, PagedQuery pagedQuery)
        {
            if (!string.IsNullOrEmpty(pagedQuery.Search))
                query = query.Where(q => (q.FirstName.ToLower() + " " + q.LastName.ToLower()).Contains(pagedQuery.Search.ToLower())
                || (q.MobileNumber.ToLower()).Contains(pagedQuery.Search.ToLower()) );

            if (pagedQuery.PageNumber > 0)
                query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

            query = query.Take(pagedQuery.PageSize);

            return query;
        }
    }
}
