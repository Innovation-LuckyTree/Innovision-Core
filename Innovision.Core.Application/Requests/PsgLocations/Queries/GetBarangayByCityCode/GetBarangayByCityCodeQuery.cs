using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.PsgLocations.Queries.GetBarangayByCityCode
{
    public record GetBarangayByCityCodeQuery(string cityCode) : IRequest<List<PsgBarangayDto>>;
    public class GetBarangayByCityCodeQueryHandler : IRequestHandler<GetBarangayByCityCodeQuery, List<PsgBarangayDto>>
    {
        private readonly IPsgBarangayVm _psgBarangayVm;

        public GetBarangayByCityCodeQueryHandler(IPsgBarangayVm psgBarangayVm)
        {
            _psgBarangayVm = psgBarangayVm;
        }

        public async Task<List<PsgBarangayDto>> Handle(GetBarangayByCityCodeQuery request, CancellationToken cancellationToken)
        {
            return _psgBarangayVm.GetBarangayByCityCode(request.cityCode).ToList();
        }
    }
}
