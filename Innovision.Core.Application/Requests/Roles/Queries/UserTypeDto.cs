using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Roles.Queries;

public class UserTypeDto : IMapFrom<UserType>
{
    public int UserTypeId { get; set; }
    public string UserTypeName { get; set; }
    public int GroupType { get; set; }
    public int RoleType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserType, UserTypeDto>()
            .ForMember(t => t.UserTypeId, f => f.MapFrom(src => src.UserTypeId))
            .ForMember(t => t.UserTypeName, f => f.MapFrom(src => src.UserTypeName))
            .ForMember(t => t.GroupType, f => f.MapFrom(src => src.GroupType))
            .ForMember(t => t.RoleType, f => f.MapFrom(src => src.RoleType));
    }
}
