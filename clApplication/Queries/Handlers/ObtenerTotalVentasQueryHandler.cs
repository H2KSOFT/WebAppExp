using clDomain.Interfaces;
using MediatR;

namespace clApplication.Queries.Handlers
{
    public class ObtenerTotalVentasQueryHandler : IRequestHandler<ObtenerTotalVentasQuery, decimal>
    {
        private readonly IOrdenRepository _ordenRepository;

        public ObtenerTotalVentasQueryHandler(IOrdenRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        public async Task<decimal> Handle(ObtenerTotalVentasQuery request, CancellationToken cancellationToken)
        {
            return await _ordenRepository.ObtenerTotalVentasPorFechaAsync(request.Fecha);
        }
    }
}
