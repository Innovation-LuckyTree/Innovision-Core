using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Faqs.Queries;

public class FaqDto : IMapFrom<FrequentlyAskQuestion>
{
    public int FrequentlyAskQuestionId { get; set; }
    public int GameId { get; set; }
    public string GameName { get; set; }
    public bool IsApplicationRelated { get; set; }
    public int OrderNo { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FrequentlyAskQuestion, FaqDto>()
            .ForMember(t => t.FrequentlyAskQuestionId, f => f.MapFrom(src => src.FrequentlyAskQuestionId))
            .ForMember(t => t.GameId, f => f.MapFrom(src => src.GameId))
            .ForMember(t => t.GameName, f => f.MapFrom(src => src.Game.Name))
            .ForMember(t => t.IsApplicationRelated, f => f.MapFrom(src => src.IsApplicationRelated))
            .ForMember(t => t.OrderNo, f => f.MapFrom(src => src.OrderNo))
            .ForMember(t => t.Question, f => f.MapFrom(src => src.Question))
            .ForMember(t => t.Answer, f => f.MapFrom(src => src.Answer));
    }
}