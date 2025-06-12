namespace Innovision.Core.Domain.Entity
{
    public class AddressCode
    {
        public long AddressCodeId { get; set; }
        public long AccountInfoId { get; set; }
        public string? RegionCode { get; set; }
        public string? ProvinceCode { get; set; }
        public string? MunicipalityCode { get; set; }
        public string? BarangayCode { get; set; }
        public string? PermRegionCode { get; set; }
        public string? PermProvinceCode { get; set; }
        public string? PermMunicipalityCode { get; set; }
        public string? PermBarangayCode { get; set; }
        public virtual Account Account { get; set; }
    }
}
