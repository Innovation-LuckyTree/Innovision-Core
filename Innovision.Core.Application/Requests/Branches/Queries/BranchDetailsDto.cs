using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Branches.Queries
{
    public class BranchDetailsDto : IMapFrom<Branch>
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public bool IsMain { get; set; }
        public string? Address { get; set; }

        public int? DashboardUserCount { get; set; }
        public int? AcountingUserCount { get; set; }
        public int? SupportUserCount { get; set; }
        public  DateTimeOffset CreatedOn { get; set; }

        public long? MasterAgentAccountId { get; set; }
        public long? AgentAccountId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Branch, BranchDetailsDto>()
                .ForMember(t => t.BranchId, f => f.MapFrom(src => src.BranchId))
                .ForMember(t => t.BranchName, f => f.MapFrom(src => src.BranchName))
                .ForMember(t => t.IsActive, f => f.MapFrom(src => src.IsActive))
                .ForMember(t => t.IsMain, f => f.MapFrom(src => src.IsMain))

                .ForMember(t => t.AgentAccountId, f => f.MapFrom(src => src.GameSiteAccountId))
                .ForMember(t => t.MasterAgentAccountId, f => f.MapFrom(src => src.GameSiteManagerId))

                .ForMember(t => t.DashboardUserCount, f => f.MapFrom(src => src.Account.Where(m => m.UserType.GroupType == 0).Count()))
                .ForMember(t => t.AcountingUserCount, f => f.MapFrom(src => src.Account.Where(m => m.UserType.GroupType == 1).Count()))
                .ForMember(t => t.SupportUserCount, f => f.MapFrom(src => src.Account.Where(m => m.UserType.GroupType == 2).Count()))

                .ForMember(t => t.Address, f => f.MapFrom(src => src.Address.Region + ", " + src.Address.Province + ", " + src.Address.Municipality + ", " + src.Address.Barangay))
                .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
                ;
        }
    }
}
