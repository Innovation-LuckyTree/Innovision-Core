using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.Queries.GetVerifiedUsers
{
    public class VerifiedUserDto : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountObjectId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string UserTypeName { get; set; }
        public string RefferralCode { get; set; }
        public string RecruiterName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, VerifiedUserDto>()
                .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
                .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
                .ForMember(t => t.FullName, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
                .ForMember(t => t.UserTypeName, f => f.MapFrom(src => src.UserType.UserTypeName))
                .ForMember(t => t.RefferralCode, f => f.MapFrom(src => src.RefferralCode))
                ;
        }
    }
}
