using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Models;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Commands.RecruiterRegistration
{
    public class RecruiterRegistrationCommand : IRequest<ApiResponse<Guid>>
    {
        public string? ReferralCode { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string NatureOfWork { get; set; }
        public string SourceOfIncome { get; set; }
        public string BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public int? BranchId { get; set; }

        public string PresentRegion { get; set; }
        public string PresentProvince { get; set; }
        public string PresentMunicipality { get; set; }
        public string PresentBarangay { get; set; }
        public string PresentStreetOrPurok { get; set; }

        public string PermanentRegion { get; set; }
        public string PermanentProvince { get; set; }
        public string PermanentMunicipality { get; set; }
        public string PermanentBarangay { get; set; }
        public string PermanentStreetOrPurok { get; set; }

        public AddressCodes? AddressCode { get; set; }

        public string ValidId { get; set; }
        public string FrontIdPath { get; set; }
        public string SelfiePath { get; set; }
    }
    public class RecruiterRegistrationCommandHandler(ICoreDbContext dbContext) : IRequestHandler<RecruiterRegistrationCommand, ApiResponse<Guid>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;

        public async Task<ApiResponse<Guid>> Handle(RecruiterRegistrationCommand request, CancellationToken cancellationToken)
        {
            var isExists = _dbContext.Accounts.Where(x => x.MobileNumber == request.MobileNumber
                    && (x.AccountStatusId == Domain.Enums.AccountStatus.ForApproval
                        || x.AccountStatusId == Domain.Enums.AccountStatus.Migrated
                        || x.AccountStatusId == Domain.Enums.AccountStatus.Approved
                        || x.AccountStatusId == Domain.Enums.AccountStatus.Block)).Any();

            if (isExists)
                return new ApiResponse<Guid>() { Success = false, ErrorMessage = $"Mobile Number:  {request.MobileNumber} already exist" };

            var accountInfo = CreateAccount(request, Guid.NewGuid(), Guid.NewGuid(), request.BranchId);

            _dbContext.Accounts.Add(accountInfo);
            await _dbContext.SaveChangesAsync();

            return new ApiResponse<Guid>() { Data = accountInfo.AccountObjectId };
        }

        private Account CreateAccount(RecruiterRegistrationCommand request, Guid acctObjId, Guid userId, int? branchId) =>
            new Account
            {
                AccountObjectId = acctObjId,
                BranchId = (branchId.HasValue) ? branchId.Value : -1,
                UserId = userId,
                RefferralCode = (!string.IsNullOrEmpty(request.ReferralCode)) ? request.ReferralCode : string.Empty,
                RefferralKey = GenerateRefferalCode.GenerateCode(8),
                MobileNumber = request.MobileNumber,
                //Commision = (request.Commission.HasValue) ? request.Commission.Value : 0,
                //SalaryRange = (request.SalaryRange.HasValue) ? request.SalaryRange.Value : null,

                FirstName = request.FirstName,
                LastName = request.LastName,
                //MiddleName = request.MiddleName,
                Nationality = request.Nationality,
                NatureOfWork = request.NatureOfWork,
                SourceOfIncome = request.SourceOfIncome,
                BirthDate = request.BirthDate,
                PlaceOfBirth = request.PlaceOfBirth,

                ValidId = request.ValidId,
                FrontIdPath = request.FrontIdPath,
                SelfiePath = request.SelfiePath,
                //BackIdPath = request.BackIdPath,
                //SignaturePath = request.SignaturePath,

                Region = request.PresentRegion,
                Province = request.PresentProvince,
                Municipality = request.PresentMunicipality,
                Barangay = request.PresentBarangay,
                StreetOrPurok = request.PresentStreetOrPurok,

                PresentRegion = request.PresentRegion,
                PresentProvince = request.PresentProvince,
                PresentMunicipality = request.PresentMunicipality,
                PresentBarangay = request.PresentBarangay,
                PresentStreetOrPurok = request.PresentStreetOrPurok,

                PermanentRegion = request.PermanentRegion,
                PermanentProvince = request.PermanentProvince,
                PermanentMunicipality = request.PermanentMunicipality,
                PermanentBarangay = request.PermanentBarangay,
                PermanentStreetOrPurok = request.PermanentStreetOrPurok,
                AddressCodes = [new AddressCode
                {
                    RegionCode = request.AddressCode.RegionCode,
                    ProvinceCode = request.AddressCode.ProvinceCode,
                    MunicipalityCode = request.AddressCode.MunicipalityCode,
                    BarangayCode = request.AddressCode.BarangayCode,
                    PermRegionCode = request.AddressCode.PermRegionCode,
                    PermProvinceCode = request.AddressCode.PermProvinceCode,
                    PermMunicipalityCode = request.AddressCode.PermMunicipalityCode,
                    PermBarangayCode = request.AddressCode.PermBarangayCode,
                }],

                IsActive = true,
                AccountStatusId = Domain.Enums.AccountStatus.ForApproval,
                UserTypeId = Domain.Enums.UserTypes.Agent,
                ForVerification = (!string.IsNullOrEmpty(request.ValidId) && !string.IsNullOrEmpty(request.FrontIdPath) && !string.IsNullOrEmpty(request.SelfiePath)) ? true : false,

                CreatedOn = DateTime.UtcNow,
                IsMain = false
            };
    }
}
