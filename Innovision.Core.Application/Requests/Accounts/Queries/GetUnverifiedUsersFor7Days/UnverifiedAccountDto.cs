using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetUnverifiedUsersFor7Days;

public class UnverifiedAccountDto : IMapFrom<Account>
{
    private readonly double _secondsPerDay = 86400;

    public long AccountId { get; set; }
    public  DateTimeOffset AccountCreated { get; set; }

    public double SecondsDifference
    {
        get
        {
            return DateTime.UtcNow.Subtract(AccountCreated.Date).TotalSeconds;
        }
    }

    public int RemainingDay
    {
        get
        {
            return (int)Math.Floor(SecondsDifference / _secondsPerDay);
        }
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, UnverifiedAccountDto>()
            .ForMember(t => t.AccountId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.AccountCreated, f => f.MapFrom(src => src.CreatedOn));
    }
}
