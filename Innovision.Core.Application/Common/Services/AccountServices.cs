using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using System.Security.Cryptography;

namespace Innovision.Core.Application.Common.Services;

public class AccountServices : IAccountServices
{
    public Account GenerateCreateUserModel(Details details, Guid userId, bool IsActive, bool IsMain, ICurrentUserService _currentUserService)
    {
        return new Account()
        {
            AccountObjectId = Guid.NewGuid(),
            UserId = userId,
            FirstName = details.FirstName,
            LastName = details.LastName,
            MiddleName = details.MiddleName,
            Gender = details.Gender,
            Email = details.Email,
            MobileNumber = details.ContactNumber,
            MartialStatus = details.MartialStatus,
            BirthDate = details.BirthDate,
            Region = details.Region,
            Province = details.Province,
            Municipality = details.Municipality,
            Barangay = details.Barangay,
            StreetOrPurok = details.StreetOrPurok,
            PermanentRegion = details.Region,
            PermanentProvince = details.Province,
            PermanentMunicipality = details.Municipality,
            PermanentBarangay = details.Barangay,
            PermanentStreetOrPurok = details.StreetOrPurok,
            UserTypeId = Domain.Enums.UserTypes.Operator,
            CreatedBy = string.IsNullOrEmpty(_currentUserService.UserId) ? "System" : _currentUserService.UserId,
            ModifiedBy = string.IsNullOrEmpty(_currentUserService.UserId) ? "System" : _currentUserService.UserId,
            AccountStatusId = Domain.Enums.AccountStatus.Approved,
            IsActive = IsActive,
            IsMain = IsMain
        };
    }

    public string GenerateCode(int string_length)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var bit_count = string_length * 6;
            var byte_count = (bit_count + 7) / 8; // rounded up
            var bytes = new byte[byte_count];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes).ToUpper();
        }
    }
}

