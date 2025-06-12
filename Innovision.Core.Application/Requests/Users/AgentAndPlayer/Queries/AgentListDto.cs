using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Queries
{
    public class AgentListDto : IMapFrom<Account>
    {
        public Guid AccountObjectId { get; set; }
        public string FullName { get; set; }
        public string Branch { get; set; }
        public string ContactNumber { get; set; }
        public  DateTimeOffset? RegistrationDate { get; set; }
        public decimal Commision { get; set; }
        public int Age { get; set; } = 0;
        public bool IsVerified { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, AgentListDto>()
              .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.FullName, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
              .ForMember(t => t.Branch, f => f.MapFrom(src => src.Branch.BranchName))
              .ForMember(t => t.ContactNumber, f => f.MapFrom(src => src.MobileNumber))
              .ForMember(t => t.RegistrationDate, f => f.MapFrom(src => src.CreatedOn))
              .ForMember(t => t.Commision, f => f.MapFrom(src => src.Commision))
              .ForMember(t => t.IsVerified, f => f.MapFrom(src => src.IsVerified))
              .ForMember(t => t.Age, f => f.MapFrom(src => src.Age))
              ;
        }
    }
}