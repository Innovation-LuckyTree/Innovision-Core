using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Games.Queries;

public class GameTypesDto : IMapFrom<GameType>
{
    public int GameTypeId { get; set; }
    public int GameId { get; set; }
    public int GameReferenceId { get; set; } // from game api
    public string GameTypeName { get; set; }
    public string GameTypeDesciption { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GameType, GameTypesDto>()
            .ForMember(t => t.GameId, f => f.MapFrom(src => src.GameId))
            .ForMember(t => t.GameTypeId, f => f.MapFrom(src => src.GameTypeId))
            .ForMember(t => t.GameReferenceId, f => f.MapFrom(src => src.GameReferenceId))
            .ForMember(t => t.GameTypeDesciption, f => f.MapFrom(src => src.GameTypeDesciption));
    }
}
