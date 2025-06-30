using System.Text.Json.Serialization;
using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Players.Queries;

public class PlayerAccountDto : IMapFrom<Account>
{
    public long AccountInfoId { get; set; }
    public Guid AccountObjectId { get; set; }
    public Guid AccountCreditId { get; set; }
    public Guid AccountBonusId { get; set; }
    public Guid UserId { get; set; }
    public string PaymentAccount { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Suffix { get; set; }
    public int? SalaryRange { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public string NatureOfWork { get; set; }
    public string SourceOfIncome { get; set; }
    public string FrontIdPath { get; set; }
    public string BackIdPath { get; set; }
    public string SignaturePath { get; set; }
    public string ProfilePath { get; set; }
    public string SelfiePath { get; set; }
    public bool IsVerified { get; set; }
    public bool IsDeclined { get; set; }
    public bool ForVerification { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public string ScreenName { get; set; }
    public int Level { get; set; } = 1;
    public string BirthDate { get; set; }
    public DateTimeOffset DateCreated { get; set; }

    [JsonIgnore]
    public string BranchStreet { get; set; }
    [JsonIgnore]
    public string BranchBarangay { get; set; }
    [JsonIgnore]
    public string BranchMunicipality { get; set; }
    [JsonIgnore]
    public string BranchProvince { get; set; }

    public string BranchAddress
    {
        get => $"{BranchStreet}, {BranchBarangay}, {BranchMunicipality}, {BranchProvince}".Trim();
    }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    // public DateTimeOffset LastPasswordChange
    // {
    //     get
    //     {
    //         return LastSetPassword ?? DateCreated;
    //     }
    // }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, PlayerAccountDto>()
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
            .ForMember(t => t.AccountCreditId, f => f.MapFrom(src => src.AccountCreditId))
            .ForMember(t => t.AccountBonusId, f => f.MapFrom(src => src.AccountBonusId))
            .ForMember(t => t.UserId, f => f.MapFrom(src => src.UserId))
            .ForMember(t => t.UserName, f => f.MapFrom(src => src.UserName))
            .ForMember(t => t.Suffix, f => f.MapFrom(src => src.Suffix))
            .ForMember(t => t.ScreenName, f => f.MapFrom(src => src.ScreenName))
            .ForMember(t => t.Level, f => f.MapFrom(src => src.Level))
            .ForMember(t => t.FirstName, f => f.MapFrom(src => src.FirstName))
            .ForMember(t => t.LastName, f => f.MapFrom(src => src.LastName))
            .ForMember(t => t.MiddleName, f => f.MapFrom(src => src.MiddleName))
            .ForMember(t => t.Email, f => f.MapFrom(src => src.Email))
            .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
            .ForMember(t => t.FrontIdPath, f => f.MapFrom(src => src.FrontIdPath))
            .ForMember(t => t.BackIdPath, f => f.MapFrom(src => src.BackIdPath))
            .ForMember(t => t.SignaturePath, f => f.MapFrom(src => src.SignaturePath))
            .ForMember(t => t.ProfilePath, f => f.MapFrom(src => src.ProfilePath))
            .ForMember(t => t.SelfiePath, f => f.MapFrom(src => src.SelfiePath))
            .ForMember(t => t.PaymentAccount, f => f.MapFrom(src => src.PaymentAccountId))
            .ForMember(t => t.BranchId, f => f.MapFrom(src => src.BranchId))
            .ForMember(t => t.BranchName, f => f.MapFrom(src => src.Branch.BranchName))
            .ForMember(t => t.BranchStreet, f => f.MapFrom(src => src.Branch.Address.StreetOrPurok))
            .ForMember(t => t.BranchBarangay, f => f.MapFrom(src => src.Branch.Address.Barangay))
            .ForMember(t => t.BranchMunicipality, f => f.MapFrom(src => src.Branch.Address.Municipality))
            .ForMember(t => t.BranchProvince, f => f.MapFrom(src => src.Branch.Address.Province))
            .ForMember(t => t.IsVerified, f => f.MapFrom(src => src.IsVerified))
            .ForMember(t => t.IsDeclined, f => f.MapFrom(src => src.IsDeclined))
            .ForMember(t => t.ForVerification, f => f.MapFrom(src => src.ForVerification))
            .ForMember(t => t.SalaryRange, f => f.MapFrom(src => src.SalaryRange))
            .ForMember(t => t.NatureOfWork, f => f.MapFrom(src => src.NatureOfWork))
            .ForMember(t => t.SourceOfIncome, f => f.MapFrom(src => src.SourceOfIncome))
            .ForMember(t => t.BirthDate, f => f.MapFrom(src => src.BirthDate))
            .ForMember(t => t.DateCreated, f => f.MapFrom(src => src.CreatedOn));
    }
}