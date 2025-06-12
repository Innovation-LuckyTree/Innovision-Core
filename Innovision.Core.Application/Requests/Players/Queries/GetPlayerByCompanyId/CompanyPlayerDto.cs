using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerByCompanyId
{
    public class CompanyPlayerDto : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountObjectId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, CompanyPlayerDto>()
                .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
                .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
                .ForMember(t => t.FullName, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
                ;
        }
    }
}
