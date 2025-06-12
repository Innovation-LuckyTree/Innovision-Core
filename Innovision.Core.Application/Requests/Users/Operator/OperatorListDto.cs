using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovision.Core.Application.Requests.Accounts.Users.Operator;
public class OperatorListDto : IMapFrom<Account>
{
    public Guid OperatorId { get; set; }
    public string OperatorName { get; set; }
    public string Branch { get; set; }
    public string ContactNumber { get; set; }
    public  DateTimeOffset? RegistrationDate { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, OperatorListDto>()
          .ForMember(t => t.OperatorId, f => f.MapFrom(src => src.AccountObjectId))
          .ForMember(t => t.OperatorName, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
          .ForMember(t => t.Branch, f => f.MapFrom(src => src.Branch.BranchName))
          .ForMember(t => t.ContactNumber, f => f.MapFrom(src => src.MobileNumber))
          .ForMember(t => t.RegistrationDate, f => f.MapFrom(src => src.CreatedOn))
          ;
    }
}