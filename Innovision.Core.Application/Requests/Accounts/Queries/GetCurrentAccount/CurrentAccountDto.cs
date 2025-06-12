using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccount
{
    public class CurrentAccountDto : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountObjectId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MartialStatus { get; set; }
        public string BloodType { get; set; }
        public string Nationality { get; set; }
        public string NatureOfWork { get; set; }
        public string SourceOfIncome { get; set; }
        public string BirthDate { get; set; }
        public string MobileNumber { get; set; }
        public int UserTypeId { get; set; }
        public bool IsMain { get; set; }
        public string RefferralKey { get; set; }
        public int AccountStatusId { get; set; }
        public string RefferralCode { get; set; }
        public string ValidId { get; set; }
        public string FrontIdPath { get; set; }
        public string BackIdPath { get; set; }
        public string SignaturePath { get; set; }
        public string ProfilePath { get; set; }
        public string SelfiePath { get; set; }
        public bool IsVerified { get; set; }
        public bool IsDecline { get; set; }
        public  DateTimeOffset? LastSetPassword { get; set; }

        public string Region { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string Barangay { get; set; }
        public string StreetOrPurok { get; set; }
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

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string CompanyName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, CurrentAccountDto>()
                .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
                .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
                .ForMember(t => t.UserId, f => f.MapFrom(src => src.UserId))
                .ForMember(t => t.FirstName, f => f.MapFrom(src => src.FirstName))
                .ForMember(t => t.LastName, f => f.MapFrom(src => src.LastName))
                .ForMember(t => t.MiddleName, f => f.MapFrom(src => src.MiddleName))
                .ForMember(t => t.Email, f => f.MapFrom(src => src.Email))
                .ForMember(t => t.Gender, f => f.MapFrom(src => src.Gender))
                .ForMember(t => t.MartialStatus, f => f.MapFrom(src => src.MartialStatus))
                .ForMember(t => t.BloodType, f => f.MapFrom(src => src.BloodType))
                .ForMember(t => t.Nationality, f => f.MapFrom(src => src.Nationality))
                .ForMember(t => t.NatureOfWork, f => f.MapFrom(src => src.NatureOfWork))
                .ForMember(t => t.SourceOfIncome, f => f.MapFrom(src => src.SourceOfIncome))
                .ForMember(t => t.BirthDate, f => f.MapFrom(src => src.BirthDate))
                .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
                .ForMember(t => t.UserTypeId, f => f.MapFrom(src => src.UserTypeId))
                .ForMember(t => t.IsMain, f => f.MapFrom(src => src.IsMain))
                .ForMember(t => t.RefferralKey, f => f.MapFrom(src => src.RefferralKey))
                .ForMember(t => t.AccountStatusId, f => f.MapFrom(src => src.AccountStatusId))
                .ForMember(t => t.ValidId, f => f.MapFrom(src => src.ValidId))
                .ForMember(t => t.FrontIdPath, f => f.MapFrom(src => src.FrontIdPath))
                .ForMember(t => t.BackIdPath, f => f.MapFrom(src => src.BackIdPath))
                .ForMember(t => t.SignaturePath, f => f.MapFrom(src => src.SignaturePath))
                .ForMember(t => t.ProfilePath, f => f.MapFrom(src => src.ProfilePath))
                .ForMember(t => t.SelfiePath, f => f.MapFrom(src => src.SelfiePath))
                .ForMember(t => t.IsVerified, f => f.MapFrom(src => src.IsVerified))
                .ForMember(t => t.IsDecline, f => f.MapFrom(src => src.IsDeclined))
                .ForMember(t => t.LastSetPassword, f => f.MapFrom(src => src.LastSetPassword))
                .ForMember(t => t.Region, f => f.MapFrom(src => src.Region))
                .ForMember(t => t.Province, f => f.MapFrom(src => src.Province))
                .ForMember(t => t.Municipality, f => f.MapFrom(src => src.Municipality))
                .ForMember(t => t.Barangay, f => f.MapFrom(src => src.Barangay))
                .ForMember(t => t.StreetOrPurok, f => f.MapFrom(src => src.StreetOrPurok))

                .ForMember(t => t.PresentRegion, f => f.MapFrom(src => src.PresentRegion))
                .ForMember(t => t.PresentProvince, f => f.MapFrom(src => src.PresentProvince))
                .ForMember(t => t.PresentMunicipality, f => f.MapFrom(src => src.PresentMunicipality))
                .ForMember(t => t.PresentBarangay, f => f.MapFrom(src => src.PresentBarangay))
                .ForMember(t => t.PresentStreetOrPurok, f => f.MapFrom(src => src.PresentStreetOrPurok))

                .ForMember(t => t.PermanentRegion, f => f.MapFrom(src => src.PermanentRegion))
                .ForMember(t => t.PermanentProvince, f => f.MapFrom(src => src.PermanentProvince))
                .ForMember(t => t.PermanentMunicipality, f => f.MapFrom(src => src.PermanentMunicipality))
                .ForMember(t => t.PermanentBarangay, f => f.MapFrom(src => src.PermanentBarangay))
                .ForMember(t => t.PermanentStreetOrPurok, f => f.MapFrom(src => src.PermanentStreetOrPurok))

                .ForMember(t => t.BranchId, f => f.MapFrom(src => src.BranchId))
                .ForMember(t => t.BranchName, f => f.MapFrom(src => src.Branch.BranchName));
        }
    }
}
