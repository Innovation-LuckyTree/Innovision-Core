using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Branches.Queries;
public class BranchDto : IMapFrom<Branch>
{
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public bool IsActive { get; set; }
    public int? NumberOfUsers { get; set; }
    public  DateTimeOffset CreatedOn { get; set; }
    public Guid BranchCreditObjectId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Branch, BranchDto>()
            .ForMember(t => t.BranchId, f => f.MapFrom(src => src.BranchId))
            .ForMember(t => t.BranchName, f => f.MapFrom(src => src.BranchName))
            .ForMember(t => t.IsActive, f => f.MapFrom(src => src.IsActive))
            .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
            .ForMember(t => t.BranchCreditObjectId, f => f.MapFrom(src => src.BranchCreditObjectId))
            .ForMember(t => t.NumberOfUsers, f => f.MapFrom(src => src.Account.Count()));
    }
}
  