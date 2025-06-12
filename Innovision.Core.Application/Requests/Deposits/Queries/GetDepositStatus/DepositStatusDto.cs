using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Deposits.Queries.GetDepositStatus;

public class DepositStatusDto : IMapFrom<DepositStatus>
{
    public int DepositStatusId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DepositStatus, DepositStatusDto>()
            .ForMember(t => t.DepositStatusId, f => f.MapFrom(src => src.DepositStatusId))
            .ForMember(t => t.Name, f => f.MapFrom(src => src.Name))
            .ForMember(t => t.Description, f => f.MapFrom(src => src.Description));
    }
}