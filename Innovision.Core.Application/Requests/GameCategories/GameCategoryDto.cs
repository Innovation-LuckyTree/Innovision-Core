using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.GameCategories.Queries;

public class GameCategoryDto : IMapFrom<GameCategory>
{
    public int GameCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<GameCategory, GameCategoryDto>()
            .ForMember(t => t.GameCategoryId, f => f.MapFrom(src => src.GameCategoryId))
            .ForMember(t => t.Name, f => f.MapFrom(src => src.Name))
            .ForMember(t => t.Description, f => f.MapFrom(src => src.Description))
            .ForMember(t => t.CoverImage, f => f.MapFrom(src => src.CoverImage));
    }
}