using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Orders.Queries;

public class JackpotWinnerAttachmentDto : IMapFrom<JackpotWinnerAttachment>
{
    public long JackpotWinnerAttachmentId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<JackpotWinnerAttachment, JackpotWinnerAttachmentDto>()
            .ForMember(t => t.JackpotWinnerAttachmentId, f => f.MapFrom(src => src.JackpotWinnerAttachmentId))
            .ForMember(t => t.FileName, f => f.MapFrom(src => src.FileName))
            .ForMember(t => t.FilePath, f => f.MapFrom(src => src.FilePath))
            .ForMember(t => t.FileType, f => f.MapFrom(src => src.FileType));
    }
}