using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlinePlayers
{
    public class DownlinePlayersDto : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountObjectId { get; set; }
        public Guid AccountCreditId { get; set; }
        public string Fullname { get; set; }
        public string BranchName { get; set; }
        public string MobileNumber { get; set; }
        public string RefferalCode { get; set; }
        public string RecruiterName { get; set; }
        public int Status { get; set; } = 1;
        public decimal? CreditBalance { get; set; }
        public  DateTimeOffset? CreditUpdatedOn { get; set; }
        public  DateTimeOffset? CreatedOn { get; set; }
        public  DateTimeOffset? ApprovedDate { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, DownlinePlayersDto>()
              .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
              .ForMember(t => t.AccountCreditId, f => f.MapFrom(src => src.AccountCreditId))
              .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.Fullname, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
              .ForMember(t => t.BranchName, f => f.MapFrom(src => src.Branch.BranchName))
              .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
              .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
              .ForMember(t => t.RefferalCode, f => f.MapFrom(src => src.RefferralCode))
              .ForMember(t => t.Status, f => f.MapFrom(src => src.IsActive))
              .ForMember(t => t.ApprovedDate, f => f.MapFrom(src => src.AccountHistories.Where(m => m.Action == "APPROVE").FirstOrDefault().CreatedOn))
              ;
        }
    }
}