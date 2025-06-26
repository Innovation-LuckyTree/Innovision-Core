using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.GameProviders;

public class GameProvidersDto : IMapFrom<GameProvider>
{
    public int GameProviderId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GameProvider, GameProvidersDto>()
            .ForMember(t => t.GameProviderId, f => f.MapFrom(src => src.GameProviderId))
            .ForMember(t => t.Name, f => f.MapFrom(src => src.Name))
            .ForMember(t => t.Description, f => f.MapFrom(src => src.Description))
            .ForMember(t => t.CoverImage, f => f.MapFrom(src => src.CoverImage));
    }
}