using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Users.Queries
{
    public class UnverifiedUsers : IMapFrom<Account>
    {
        public long AccountInfoId { get; set; }
        public Guid AccountObjectId { get; set; }
        public Guid UserId { get; set; }
        public string Fullname { get; set; }
        public int BranchId { get; set; }
        public string Branch { get; set; }
        public string RefferralCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string BirthDate { get; set; }
        public string MobileNumber { get; set; }

        public  DateTimeOffset? CreatedOn { get; set; }

        public string Region { get; set; }
        public string Province { get; set; }
        public string? PresentRegion { get; set; }
        public string? PresentProvince { get; set; }
        public string? PresentMunicipality { get; set; }
        public string? PresentBarangay { get; set; }
        public string? PresentStreetOrPurok { get; set; }

        public string? PermanentRegion { get; set; }
        public string? PermanentProvince { get; set; }
        public string? PermanentMunicipality { get; set; }
        public string? PermanentBarangay { get; set; }
        public string? PermanentStreetOrPurok { get; set; }
        public string? ValidId { get; set; }
        public string? FrontIdPath { get; set; }
        public string? BackIdPath { get; set; }
        public string? SelfiePath { get; set; }
        public string? CivilStatus { get; set; }
        public string? Sex { get; set; }
        public string? Nationality { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? SourceOfIncome { get; set; }
        public string? NatureOfWork { get; set; }
        public int? SalaryRange { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Account, UnverifiedUsers>()
              .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
              .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
              .ForMember(t => t.UserId, f => f.MapFrom(src => src.UserId))
              .ForMember(t => t.Fullname, f => f.MapFrom(src => src.FirstName + " " + src.LastName))
              .ForMember(t => t.BranchId, f => f.MapFrom(src => src.Branch.BranchId))
              .ForMember(t => t.Branch, f => f.MapFrom(src => src.Branch.BranchName))
              .ForMember(t => t.BirthDate, f => f.MapFrom(src => src.BirthDate))

              .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
              .ForMember(t => t.FirstName, f => f.MapFrom(src => src.FirstName))
              .ForMember(t => t.LastName, f => f.MapFrom(src => src.LastName))
              .ForMember(t => t.MiddleName, f => f.MapFrom(src => src.MiddleName))

              .ForMember(t => t.Region, f => f.MapFrom(src => src.Region))
              .ForMember(t => t.Province, f => f.MapFrom(src => src.Province))
              .ForMember(t => t.PresentRegion, f => f.MapFrom(src => src.PresentRegion))
              .ForMember(t => t.PresentProvince, f => f.MapFrom(src => src.PresentProvince))
              .ForMember(t => t.PresentMunicipality, f => f.MapFrom(src => src.PresentMunicipality))
              .ForMember(t => t.PresentBarangay, f => f.MapFrom(src => src.PresentBarangay))
              .ForMember(t => t.PermanentRegion, f => f.MapFrom(src => src.PermanentRegion))
              .ForMember(t => t.PermanentProvince, f => f.MapFrom(src => src.PermanentProvince))
              .ForMember(t => t.PermanentMunicipality, f => f.MapFrom(src => src.PermanentMunicipality))
              .ForMember(t => t.PermanentBarangay, f => f.MapFrom(src => src.PermanentBarangay))
              .ForMember(t => t.ValidId, f => f.MapFrom(src => src.ValidId))
              .ForMember(t => t.FrontIdPath, f => f.MapFrom(src => src.FrontIdPath))
              .ForMember(t => t.BackIdPath, f => f.MapFrom(src => src.BackIdPath))
              .ForMember(t => t.SelfiePath, f => f.MapFrom(src => src.SelfiePath))
              .ForMember(t => t.CivilStatus, f => f.MapFrom(src => src.MartialStatus))
              .ForMember(t => t.Sex, f => f.MapFrom(src => src.Gender))
              .ForMember(t => t.Nationality, f => f.MapFrom(src => src.Nationality))
              .ForMember(t => t.PlaceOfBirth, f => f.MapFrom(src => src.PlaceOfBirth))
              .ForMember(t => t.SourceOfIncome, f => f.MapFrom(src => src.SourceOfIncome))
              .ForMember(t => t.NatureOfWork, f => f.MapFrom(src => src.NatureOfWork))
              .ForMember(t => t.SalaryRange, f => f.MapFrom(src => src.SalaryRange))
              ;
        }

    }
}