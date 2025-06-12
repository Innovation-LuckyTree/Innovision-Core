using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public class Address :AuditableEntity
{
    public string? Region { get; set; }
    public string? Province { get; set; }
    public string? Municipality { get; set; }
    public string? Barangay { get; set; }
    public string? StreetOrPurok { get; set; }
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
}
