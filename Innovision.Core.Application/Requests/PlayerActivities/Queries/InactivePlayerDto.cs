using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries
{
    public class InactivePlayerDto : IMapFrom<PlayerActivity>
    {
        public long ActivityId { get; set; }
        public long AccountInfoId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public int MissedDraws { get; set; } = 0;
        public int Extended { get; set; } = 0;
        public bool RequiredTopay { get; set; }
        public int? StandardMissedDraw { get; set; }
        public  DateTimeOffset? ExcludeDateTime { get; set; }
        public  DateTimeOffset? LastDrawDateTime { get; set; }
        public TimeSpan? LastDrawTime { get; set; }
        public bool IsActive { get; set; } = true;
        public  DateTimeOffset CreatedOn { get; set; }
        public  DateTimeOffset? LastModified { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PlayerActivity, InactivePlayerDto>()
                .ForMember(t => t.ActivityId, f => f.MapFrom(src => src.ActivityId))
                .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
                .ForMember(t => t.FullName, f => f.MapFrom(src => src.Account.FirstName + " " + src.Account.LastName))
                .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.Account.MobileNumber))
                .ForMember(t => t.MissedDraws, f => f.MapFrom(src => src.MissedDraws))
                .ForMember(t => t.Extended, f => f.MapFrom(src => src.Extended))
                .ForMember(t => t.RequiredTopay, f => f.MapFrom(src => src.RequiredTopay))
                .ForMember(t => t.LastDrawDateTime, f => f.MapFrom(src => src.LastDrawDateTime))
                .ForMember(t => t.LastDrawTime, f => f.MapFrom(src => src.LastDrawTime))
                .ForMember(t => t.IsActive, f => f.MapFrom(src => src.IsActive))
                .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn))
                .ForMember(t => t.LastModified, f => f.MapFrom(src => src.LastModified))
                ;
        }
    }
}
