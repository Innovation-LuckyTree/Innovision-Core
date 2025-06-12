using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.AccountApproval.Queries.GetUsersForApprove
{
    public class UsersForApprovedDto : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountObjectId { get; set; }
        public string Fullname { get; set; }
        public string ContactNumber { get; set; }
        public  DateTimeOffset? CreatedOn { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, UsersForApprovedDto>()
              .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
              .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.Fullname, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
              .ForMember(t => t.ContactNumber, f => f.MapFrom(src => src.MobileNumber))
              .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
              ;
        }
    }
}