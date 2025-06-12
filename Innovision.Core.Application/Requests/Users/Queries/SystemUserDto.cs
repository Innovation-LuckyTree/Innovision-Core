using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.Queries
{
    public class SystemUserDto : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid UserId { get; set; }
        public Guid AccountObjectId { get; set; }
        public string Fullname { get; set; }
        public string Branch { get; set; }
        public string ContactNumber { get; set; }
        public string RoleName { get; set; }
        public bool IsMain { get; set; }
        public string RefferralCode { get; set; }
        public string RefferralKey { get; set; }
        public int UserTypeId { get; set; }
        public  DateTimeOffset? CreatedOn { get; set; }
        public Guid BranchCreditObjectId { get; set; }
        public int BranchId { get; set; }
        public bool IsVerified { get; set; }
        public string ProfilePath { get; set; }
        public string SelfiePath { get; set; }
        public int RoleType { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, SystemUserDto>()
              .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
              .ForMember(t => t.UserId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.Fullname, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
              .ForMember(t => t.Branch, f => f.MapFrom(src => src.Branch.BranchName))
              .ForMember(t => t.ContactNumber, f => f.MapFrom(src => src.MobileNumber))
              .ForMember(t => t.IsMain, f => f.MapFrom(src => src.IsMain))
              .ForMember(t => t.RefferralKey, f => f.MapFrom(src => src.RefferralKey))
              .ForMember(t => t.RefferralCode, f => f.MapFrom(src => src.RefferralCode))
              .ForMember(t => t.UserTypeId, f => f.MapFrom(src => src.UserTypeId))
              .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
              .ForMember(t => t.RoleName, f => f.MapFrom(src => src.UserType.UserTypeName))
              .ForMember(t => t.RoleName, f => f.MapFrom(src => src.UserType.RoleType))
              .ForMember(t => t.BranchCreditObjectId, f => f.MapFrom(src => src.Branch.BranchCreditObjectId))
              .ForMember(t => t.BranchId, f => f.MapFrom(src => src.Branch.BranchId))
              .ForMember(t => t.IsVerified, f => f.MapFrom(src => src.IsVerified))
              .ForMember(t => t.SelfiePath, f => f.MapFrom(src => src.SelfiePath))
              .ForMember(t => t.ProfilePath, f => f.MapFrom(src => src.ProfilePath))
              ;
        }
    }
}
