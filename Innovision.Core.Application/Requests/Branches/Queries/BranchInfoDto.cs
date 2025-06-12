using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Branches.Queries
{
    public class BranchInfoDto : IMapFrom<Branch>
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public string Region { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public  DateTimeOffset CreatedOn { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Branch, BranchInfoDto>()
                .ForMember(t => t.BranchId, f => f.MapFrom(src => src.BranchId))
                .ForMember(t => t.BranchName, f => f.MapFrom(src => src.BranchName))
                .ForMember(t => t.IsActive, f => f.MapFrom(src => src.IsActive))
                .ForMember(t => t.Region, f => f.MapFrom(src => src.Region))
                .ForMember(t => t.Province, f => f.MapFrom(src => src.Province))
                .ForMember(t => t.Municipality, f => f.MapFrom(src => src.Municipality))
                .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn));
        }
    }
}
