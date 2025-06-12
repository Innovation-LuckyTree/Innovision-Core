using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Requests.Users.Queries.GetUserForVerification;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.Queries.GetAgentsByBranchId
{
    public class UserBasicDto : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountObjectId { get; set; }
        public string FullName { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, UserBasicDto>()
                .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
                .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
                .ForMember(t => t.FullName, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
                ;
        }
    }
}
