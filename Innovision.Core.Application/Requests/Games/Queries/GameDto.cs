using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Games.Queries;

public class GameDto : IMapFrom<Game>
{
    public int GameId { get; set; }
    public Guid GameObjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }
    public IEnumerable<GameTypesDto> GameTypes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Game, GameDto>()
            .ForMember(t => t.GameId, f => f.MapFrom(src => src.GameId))
            .ForMember(t => t.GameObjectId, f => f.MapFrom(src => src.GameObjectId))
            .ForMember(t => t.Name, f => f.MapFrom(src => src.Name))
            .ForMember(t => t.Description, f => f.MapFrom(src => src.Description))
            .ForMember(t => t.Active, f => f.MapFrom(src => src.Active))
            .ForMember(t => t.GameTypes, f => f.MapFrom(src => src.GameTypes));
    }
}
