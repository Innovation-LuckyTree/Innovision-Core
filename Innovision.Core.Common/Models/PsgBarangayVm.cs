using Innovision.Core.Common.Interfaces;

namespace Innovision.Core.Common.Models
{
    public record PsgBarangayVm(IEnumerable<PsgBarangay> PsgBarangays) : IPsgBarangayVm
    {
        public IEnumerable<PsgBarangayDto> GetBarangayByCityCode(string cityCode)
        {
            return PsgBarangays.Where(o => o.CityCode == cityCode)
            .Select(m => new PsgBarangayDto 
            {
                Code = m.Code,
                Name = m.Name
            }).ToList();
        }

        public IEnumerable<PsgBarangayDto> GetBarangayByProvinceCode(string provinceCode)
        {
            return PsgBarangays.Where(o => o.ProvinceCode == provinceCode).Select(m => new PsgBarangayDto
            {
                Code = m.Code,
                Name = m.Name
            }).ToList();
        }

        public IEnumerable<PsgBarangayDto> GetBarangayByMunicipalityCode(string municipalityCode)
        {
            return PsgBarangays.Where(o => o.MunicipalityCode == municipalityCode).Select(m => new PsgBarangayDto
            {
                Code = m.Code,
                Name = m.Name
            }).ToList();
        }
    }
}
