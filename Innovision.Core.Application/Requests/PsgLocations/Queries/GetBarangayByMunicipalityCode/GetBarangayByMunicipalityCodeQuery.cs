using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.PsgLocations.Queries.GetBarangayByMunicipalityCode
{
    public record GetBarangayByMunicipalityCodeQuery(string municipalityCode) : IRequest<List<PsgBarangayDto>>;
    public class GetBarangayByMunicipalityCodeQueryHandler : IRequestHandler<GetBarangayByMunicipalityCodeQuery, List<PsgBarangayDto>>
    {
        private readonly IPsgBarangayVm _psgBarangayVm;

        public GetBarangayByMunicipalityCodeQueryHandler(IPsgBarangayVm psgBarangayVm)
        {
            _psgBarangayVm = psgBarangayVm;
        }

        public async Task<List<PsgBarangayDto>> Handle(GetBarangayByMunicipalityCodeQuery request, CancellationToken cancellationToken)
        {
            return _psgBarangayVm.GetBarangayByMunicipalityCode(request.municipalityCode).ToList();
        }
    }
}
