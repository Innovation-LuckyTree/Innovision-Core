using Innovision.Core.Common.Models;

namespace Innovision.Core.Common.Interfaces
{
    public interface IPsgBarangayVm
    {
        IEnumerable<PsgBarangayDto> GetBarangayByCityCode(string cityCode);
        IEnumerable<PsgBarangayDto> GetBarangayByProvinceCode(string provinceCode);
        IEnumerable<PsgBarangayDto> GetBarangayByMunicipalityCode(string municipalityCode);
    }
}
