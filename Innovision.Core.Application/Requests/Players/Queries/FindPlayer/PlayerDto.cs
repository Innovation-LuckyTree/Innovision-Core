using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Players.Queries.FindPlayer;

public class PlayerDto : IMapFrom<Account>
{
    public Guid UserId { get; set; }
    public Guid AccountId { get; set; }
    public string Name { get; set; }
    public int AccountStatus { get; set; }
    public bool IsVerified { get; set; }
    public  DateTimeOffset CreatedOn { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, PlayerDto>()
            .ForMember(t => t.AccountId, f => f.MapFrom(src => src.AccountObjectId))
            .ForMember(t => t.UserId, f => f.MapFrom(src => src.UserId))
            .ForMember(t => t.Name, f => f.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(t => t.AccountStatus, f => f.MapFrom(src => src.AccountStatusId))
            .ForMember(t => t.IsVerified, f => f.MapFrom(src => src.IsVerified))
            .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn));
    }
}
