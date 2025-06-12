using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Branches.Queries;

public class BranchOperatorDto : IMapFrom<Account>
{
    public string OperatorName { get; set; }
    public string BranchContact { get; set; }
    public  DateTimeOffset CreatedOn { get; set; }
    public bool IsMain { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, BranchOperatorDto>()
            .ForMember(t => t.OperatorName, f => f.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
            .ForMember(t => t.IsMain, f => f.MapFrom(src => src.IsMain))
            .ForMember(t => t.BranchContact, f => f.MapFrom(src => src.MobileNumber));
    }

}
