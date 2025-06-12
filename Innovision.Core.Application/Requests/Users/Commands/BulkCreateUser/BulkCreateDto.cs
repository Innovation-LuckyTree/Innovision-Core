using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;

namespace Innovision.Core.Application.Requests.Users.Commands.BulkCreateUser
{
    public class BulkCreateDto : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountObjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string RefferralKey { get; set; }
        public string RefferralCode { get; set; }
        public string ContactNumber { get; set; }
        public decimal Commission { get; set; }
        public decimal UplineCommission { get; set; }
        public int? FmTypeId { get; set; }
        public int UserTypeId { get; set; }
        public int BranchId { get; set; }
        public  DateTimeOffset? RegistrationDate { get; set; }
        public string Type
        {
            get
            {
                return UserTypeId == UserTypes.MasterAgent ? "FirmManager"
                    : UserTypeId == UserTypes.Agent ? "Agent"
                    : "Player";
            }
        }
        public string Tag
        {
            get
            {
                return (FmTypeId.HasValue && FmTypeId == 1) ? "FM"
                    : (FmTypeId.HasValue && FmTypeId == 2) ? "PM"
                    : (FmTypeId.HasValue && FmTypeId == 3) ? "MM"
                    : (FmTypeId.HasValue && FmTypeId == 4) ? "GFM"
                    : UserTypeId == UserTypes.Agent ? "A"
                    : "P";
            }
        }
        public bool IsFirmManager
        {
            get
            {
                return UserTypeId == UserTypes.MasterAgent ? true : false;
            }
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, BulkCreateDto>()
              .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
              .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.FullName, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
              .ForMember(t => t.RefferralKey, f => f.MapFrom(src => src.RefferralKey))
              .ForMember(t => t.RefferralCode, f => f.MapFrom(src => src.RefferralCode))
              .ForMember(t => t.FirstName, f => f.MapFrom(src => src.FirstName))
              .ForMember(t => t.LastName, f => f.MapFrom(src => src.LastName))
              .ForMember(t => t.MiddleName, f => f.MapFrom(src => src.MiddleName))
              .ForMember(t => t.ContactNumber, f => f.MapFrom(src => src.MobileNumber))
              .ForMember(t => t.BranchId, f => f.MapFrom(src => src.BranchId))
              .ForMember(t => t.RegistrationDate, f => f.MapFrom(src => src.CreatedOn))
              .ForMember(t => t.FmTypeId, f => f.MapFrom(src => src.FmTypeId))
              .ForMember(t => t.UserTypeId, f => f.MapFrom(src => src.UserTypeId))
              .ForMember(t => t.Commission, f => f.MapFrom(src => src.Commision))
              ;
        }
    }
}
