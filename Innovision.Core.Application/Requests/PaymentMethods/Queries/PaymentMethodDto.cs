using AutoMapper;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Application.Common.Interfaces;

namespace Innovision.Core.Application.Requests.PaymentMethods.Queries;

public class PaymentMethodDto : IMapFrom<PaymentMethod>
{
    public int PaymentMethodId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentMethod, PaymentMethodDto>()
            .ForMember(t => t.PaymentMethodId, f => f.MapFrom(src => src.PaymentMethodId))
            .ForMember(t => t.Name, f => f.MapFrom(src => src.Name))
            .ForMember(t => t.Description, f => f.MapFrom(src => src.Description));
    }
}