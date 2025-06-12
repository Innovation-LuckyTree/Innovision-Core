using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;

namespace Innovision.Core.Application.Requests.Users.Queries.GetUserUpline
{
    public class UserUplineDto : IMapFrom<Account>
    {
        public Guid AccountObjectId { get; set; }
        public string FullName { get; set; }
        public int UserTypeId { get; set; }
        public string RoleName {
            get
            {
                return UserTypeId == (int)UserTypes.SuperAdmin ? "Super Admin"
                    : UserTypeId == (int)UserTypes.Operator ? "Operator"
                    : UserTypeId == (int)UserTypes.MasterAgent ? "Master Agent" : "Agent";
            }
        }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, UserUplineDto>()
                .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
                .ForMember(t => t.FullName, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(t => t.UserTypeId, f => f.MapFrom(src => src.UserTypeId))
                ;
        }
    }
}
