using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Games.Queries;

public class GameTypesDto : IMapFrom<GameType>
{

    public int GameTypeId { get; set; }
    public string GameTypeName { get; set; }
    public string GameTypeDescription { get; set; }
    public string CoverImage { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<GameType, GameTypesDto>()
            .ForMember(t => t.GameTypeId, f => f.MapFrom(src => src.GameTypeId))
            .ForMember(t => t.GameTypeName, f => f.MapFrom(src => src.GameTypeName))
            .ForMember(t => t.GameTypeDescription, f => f.MapFrom(src => src.GameTypeDescription))
            .ForMember(t => t.CoverImage, f => f.MapFrom(src => src.CoverImage));
    }
}
