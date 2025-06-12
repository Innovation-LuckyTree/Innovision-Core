using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Accounts.Users.MasterAgent
{
    public class MasterAgentListDto : IMapFrom<Account>
    {
        public Guid AccountObjectId { get; set; }
        public string FullName { get; set; }
        public string Branch { get; set; }
        public string ContactNumber { get; set; }
        public  DateTimeOffset? RegistrationDate { get; set; }
        public int AgentCount { get; set; } = 0;


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, MasterAgentListDto>()
              .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.FullName, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
              .ForMember(t => t.Branch, f => f.MapFrom(src => src.Branch.BranchName))
              .ForMember(t => t.ContactNumber, f => f.MapFrom(src => src.MobileNumber))
              .ForMember(t => t.RegistrationDate, f => f.MapFrom(src => src.CreatedOn))
              ;
        }
    }
}
