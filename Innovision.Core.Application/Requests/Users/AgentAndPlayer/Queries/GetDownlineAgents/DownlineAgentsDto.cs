using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlineAgents
{
    public class DownlineAgentsDto : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountObjectId { get; set; }
        public string Fullname { get; set; }
        public string MobileNumber { get; set; }
        public string RecruiterName { get; set; }
        public string BranchName { get; set; }
        public string RefferalCode { get; set; }
        public string RefferalKey { get; set; }
        public int AgentsCount { get; set; } = 0;
        public decimal Commission { get; set; } = 0;
        public decimal Amount { get; set; } = 0;
        public int PlayersCount { get; set; } = 0;
        public int Status { get; set; } = 1;
        public  DateTimeOffset? CreatedOn { get; set; }
        public  DateTimeOffset? ApprovedDate { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, DownlineAgentsDto>()
              .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
              .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.Fullname, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
              .ForMember(t => t.BranchName, f => f.MapFrom(src => src.Branch.BranchName))
              .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
              .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
              .ForMember(t => t.Commission, f => f.MapFrom(src => src.Commision))
              //.ForMember(t => t.ApprovedDate, f => f.MapFrom(src => src.AccountHistories.Where(m => m.Action == "APPROVE").FirstOrDefault().CreatedOn))
              .ForMember(t => t.Status, f => f.MapFrom(src => src.IsActive))
              .ForMember(t => t.RefferalKey, f => f.MapFrom(src => src.RefferralKey))
              .ForMember(t => t.RefferalCode, f => f.MapFrom(src => src.RefferralCode))
              ;
        }
    }
}