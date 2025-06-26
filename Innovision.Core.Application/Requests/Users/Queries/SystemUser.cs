using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.Queries
{
    public class SystemUser : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountCreditId { get; set; }
        public Guid AccountObjectId { get; set; }
        public Guid AccountBonusId { get; set; }
        public Guid UserId { get; set; }
        public string Fullname { get; set; }
        public int BranchId { get; set; }
        public string Branch { get; set; }
        public int? SalaryRange { get; set; }
        public string RefferralKey { get; set; }
        public string RefferralCode { get; set; }
        public int FmTypeId { get; set; }
        public string FMTypeName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MartialStatus { get; set; }
        public string BloodType { get; set; }
        public string Nationality { get; set; }
        public string NatureOfWork { get; set; }
        public string SourceOfIncome { get; set; }
        public string PlaceOfBirth { get; set; }
        public string BirthDate { get; set; }
        public string MobileNumber { get; set; }
        public string ValidId { get; set; }
        public string FrontIdPath { get; set; }
        public string BackIdPath { get; set; }
        public string SignaturePath { get; set; }
        public string ProfilePath { get; set; }
        public string SelfiePath { get; set; }
        public  DateTimeOffset? CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public bool IsDeclined { get; set; }
        public int? UserStatus { get; set; }
        public int? UserSubStatus { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Remarks { get; set; }

        public RecruiterDetail? RecruiterDetail { get; set; }
        public bool AdministrativeExclusion { get; set; }
        public bool SelfLimit { get; set; }
        public bool BlockedUserHistory { get; set; }
        public bool LockedUser { get; set; }
        public decimal Commission { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, SystemUser>()
              .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
              .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.AccountCreditId, f => f.MapFrom(src => src.AccountCreditId))
              .ForMember(t => t.AccountBonusId, f => f.MapFrom(src => src.AccountBonusId))
              .ForMember(t => t.UserId, f => f.MapFrom(src => src.UserId))
              .ForMember(t => t.Fullname, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
              .ForMember(t => t.BranchId, f => f.MapFrom(src => src.Branch.BranchId))
              .ForMember(t => t.Branch, f => f.MapFrom(src => src.Branch.BranchName))
              .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
              .ForMember(t => t.IsActive, f => f.MapFrom(src => src.IsActive))
              .ForMember(t => t.IsVerified, f => f.MapFrom(src => src.IsVerified))
              .ForMember(t => t.IsDeclined, f => f.MapFrom(src => src.IsDeclined))
              
              .ForMember(t => t.UserStatus, f => f.MapFrom(src => src.UserStatuses.FirstOrDefault().Status))
              .ForMember(t => t.UserSubStatus, f => f.MapFrom(src => src.UserStatuses.FirstOrDefault().SubStatus))

              .ForMember(t => t.RoleName, f => f.MapFrom(src => src.UserType.UserTypeName))
              .ForMember(t => t.RoleId, f => f.MapFrom(src => src.UserType.UserTypeId))

              .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
              .ForMember(t => t.FirstName, f => f.MapFrom(src => src.FirstName))
              .ForMember(t => t.LastName, f => f.MapFrom(src => src.LastName))
              .ForMember(t => t.MiddleName, f => f.MapFrom(src => src.MiddleName))
              .ForMember(t => t.Email, f => f.MapFrom(src => src.Email))
              .ForMember(t => t.Age, f => f.MapFrom(src => src.Age))
              .ForMember(t => t.Gender, f => f.MapFrom(src => src.Gender))
              .ForMember(t => t.MartialStatus, f => f.MapFrom(src => src.MartialStatus))
              .ForMember(t => t.BloodType, f => f.MapFrom(src => src.BloodType))
              .ForMember(t => t.Nationality, f => f.MapFrom(src => src.Nationality))
              .ForMember(t => t.NatureOfWork, f => f.MapFrom(src => src.NatureOfWork))
              .ForMember(t => t.SourceOfIncome, f => f.MapFrom(src => src.SourceOfIncome))
              .ForMember(t => t.PlaceOfBirth, f => f.MapFrom(src => src.PlaceOfBirth))
              .ForMember(t => t.ValidId, f => f.MapFrom(src => src.ValidId))
              .ForMember(t => t.FrontIdPath, f => f.MapFrom(src => src.FrontIdPath))
              .ForMember(t => t.BackIdPath, f => f.MapFrom(src => src.BackIdPath))
              .ForMember(t => t.SignaturePath, f => f.MapFrom(src => src.SignaturePath))
              .ForMember(t => t.ProfilePath, f => f.MapFrom(src => src.ProfilePath))
              .ForMember(t => t.SelfiePath, f => f.MapFrom(src => src.SelfiePath))
              .ForMember(t => t.SalaryRange, f => f.MapFrom(src => src.SalaryRange))
              .ForMember(t => t.RefferralKey, f => f.MapFrom(src => src.RefferralKey))
              .ForMember(t => t.RefferralCode, f => f.MapFrom(src => src.RefferralCode))
              .ForMember(t => t.FmTypeId, f => f.MapFrom(src => src.FmTypeId))


              .ForMember(t => t.Remarks, f => f.MapFrom(src => src.Remarks))

              .ForMember(t => t.AdministrativeExclusion, f => f.MapFrom(src => src.AdministrativeExclusions.Where(x => x.Status == 1).Any()))
              .ForMember(t => t.SelfLimit, f => f.MapFrom(src => src.SelfLimits.Any()))
              .ForMember(t => t.BlockedUserHistory, f => f.MapFrom(src => src.BlockedUserHistories.Where(m=>m.IsActive == 1).Any()))
              .ForMember(t => t.Commission, f => f.MapFrom(src => src.Commision))
              ;
        }

        public int Category { 
            get
            {
                return (IsActive) ? 1 : 0;
            }
        }
    }

    public class RecruiterDetail
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string RoleName { get; set; }
        public string GameSite { get; set; }
        public string SelfiePath { get; set; }
        public string ProfilePath { get; set; }
    }
}
