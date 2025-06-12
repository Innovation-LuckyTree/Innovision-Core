using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.Queries.GetUserForVerification
{
    public class UserVerificationDto : IMapFrom<Account>
    {
        public Guid AccountObjectId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public bool IsVerified { get; set; }
        public string Recruiter { get; set; }
        public string GameSite { get; set; }
        public Guid? RecruiterAccountObjId { get; set; }
        public string RefferralCode { get; set; }
        public  DateTimeOffset RegistrationDate { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, UserVerificationDto>()
                .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
                .ForMember(t => t.FullName, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
                .ForMember(t => t.IsVerified, f => f.MapFrom(src => src.IsVerified))
                .ForMember(t => t.GameSite, f => f.MapFrom(src => src.Branch.BranchName))
                .ForMember(t => t.RefferralCode, f => f.MapFrom(src => src.RefferralCode))
                .ForMember(t => t.RegistrationDate, f => f.MapFrom(src => src.CreatedOn));
        }
    }
}