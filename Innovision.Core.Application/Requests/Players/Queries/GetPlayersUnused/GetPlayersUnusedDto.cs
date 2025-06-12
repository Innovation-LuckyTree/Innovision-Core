using System.Text.Json.Serialization;
using AutoMapper;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Requests.Players.Queries;

public class GetPlayersUnusedDto : IMapFrom<Account>
{
    public long AccountInfoId { get; set; }
    public Guid AccountObjectId { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string MobileNumber { get; set; }
    public string ProfilePath { get; set; }
    public string FrontIdPath { get; set; }
    public string BackIdPath { get; set; }
    public bool IsVerified { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    [JsonIgnore]
    public  DateTimeOffset DateCreated { get; set; }
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


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Account, GetPlayersUnusedDto>()
            .ForMember(t => t.AccountInfoId, f => f.MapFrom(src => src.AccountInfoId))
            .ForMember(t => t.AccountObjectId, f => f.MapFrom(src => src.AccountObjectId))
            .ForMember(t => t.UserId, f => f.MapFrom(src => src.UserId))
            .ForMember(t => t.FirstName, f => f.MapFrom(src => src.FirstName))
            .ForMember(t => t.LastName, f => f.MapFrom(src => src.LastName))
            .ForMember(t => t.MiddleName, f => f.MapFrom(src => src.MiddleName))
            .ForMember(t => t.MobileNumber, f => f.MapFrom(src => src.MobileNumber))
            .ForMember(t => t.ProfilePath, f => f.MapFrom(src => src.ProfilePath))
            .ForMember(t => t.FrontIdPath, f => f.MapFrom(src => src.FrontIdPath))
            .ForMember(t => t.BackIdPath, f => f.MapFrom(src => src.BackIdPath))
            .ForMember(t => t.BranchId, f => f.MapFrom(src => src.BranchId))
            .ForMember(t => t.BranchName, f => f.MapFrom(src => src.Branch.BranchName))
            .ForMember(t => t.BranchStreet, f => f.MapFrom(src => src.Branch.StreetOrPurok))
            .ForMember(t => t.BranchBarangay, f => f.MapFrom(src => src.Branch.Barangay))
            .ForMember(t => t.BranchMunicipality, f => f.MapFrom(src => src.Branch.Municipality))
            .ForMember(t => t.BranchProvince, f => f.MapFrom(src => src.Branch.Province))
            .ForMember(t => t.IsVerified, f => f.MapFrom(src => src.IsVerified))
            .ForMember(t => t.DateCreated, f => f.MapFrom(src => src.CreatedOn));
    }
}