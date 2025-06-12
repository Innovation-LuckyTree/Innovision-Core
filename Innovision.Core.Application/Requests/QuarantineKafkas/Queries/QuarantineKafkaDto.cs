using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.QuarantineKafkas.Queries
{
  public class QuarantineKafkaDto : IMapFrom<QuarantineKafka>
  {
    public long QuarantineKafkaId { get; set; }
    public string KafkaValue { get; set; }
    public string KafkaTopic { get; set; }
    public int Attempts { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public int Status { get; set; } // 1 - active, 2 - completed, 3 - hopeless
    public  DateTimeOffset CreatedOn { get; set; }
    public  DateTimeOffset AttemptedOn { get; set; }
    public  DateTimeOffset CompletedOn { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<QuarantineKafka, QuarantineKafkaDto>()
          .ForMember(t => t.QuarantineKafkaId, f => f.MapFrom(src => src.QuarantineKafkaId))
          .ForMember(t => t.KafkaValue, f => f.MapFrom(src => src.KafkaValue))
          .ForMember(t => t.KafkaTopic, f => f.MapFrom(src => src.KafkaTopic))
          .ForMember(t => t.Attempts, f => f.MapFrom(src => src.Attempts))
          .ForMember(t => t.ErrorCode, f => f.MapFrom(src => src.ErrorCode))
          .ForMember(t => t.ErrorMessage, f => f.MapFrom(src => src.ErrorMessage))
          .ForMember(t => t.Status, f => f.MapFrom(src => src.Status))
          .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
          .ForMember(t => t.AttemptedOn, f => f.MapFrom(src => src.AttemptedOn))
          .ForMember(t => t.CompletedOn, f => f.MapFrom(src => src.CompletedOn));
    }
  }
}
