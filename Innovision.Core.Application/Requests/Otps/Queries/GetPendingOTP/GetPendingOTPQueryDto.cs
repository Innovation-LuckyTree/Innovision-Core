using AutoMapper;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Application.Common.Interfaces;

namespace Core.Application.Request.Otps.Queries.GetPendingOTP
{
    public class GetPendingOTPQueryDto : IMapFrom<OTP>
    {
        public long ReferenceId { get; set; }
        public string MobileNumber { get; set; }
        public string Code { get; set; }
        public bool IsVerify { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OTP, GetPendingOTPQueryDto>()
                .ForMember(t => t.ReferenceId, f => f.MapFrom(src => src.OtpID))
                .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
                .ForMember(t => t.Code, f => f.MapFrom(src => src.Code))
                .ForMember(t => t.IsVerify, f => f.MapFrom(src => src.IsVerify));
        }
    }
}
