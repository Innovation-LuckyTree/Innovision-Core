using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.Queries;

public class UserStatusDto : IMapFrom<Account>
{
    public long AccountInfoId { get; set; }
    public Guid AccountObjectId { get; set; }
    public Guid UserId { get; set; }
    public string Fullname { get; set; }
    public string Branch { get; set; }
    public string ContactNumber { get; set; }
    public string RoleName { get; set; }
    public int BetCount { get; set; }
    public int AccountStatus { get; set; }
    public  DateTimeOffset? CreatedOn { get; set; }
    public  DateTimeOffset? LockedDate { get; set; }
    public  DateTimeOffset? LastModified { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, UserStatusDto>()
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
            .ForMember(t => t.UserId, f => f.MapFrom(src => src.UserId))
            .ForMember(t => t.Fullname, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
            .ForMember(t => t.Branch, f => f.MapFrom(src => src.Branch.BranchName))
            .ForMember(t => t.ContactNumber, f => f.MapFrom(src => src.MobileNumber))
            .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
            .ForMember(t => t.LastModified, f => f.MapFrom(src => src.LastModified))
            .ForMember(t => t.RoleName, f => f.MapFrom(src => src.UserType.UserTypeName))
            .ForMember(t => t.AccountStatus, f => f.MapFrom(src => src.AccountStatusId));
    }
}
