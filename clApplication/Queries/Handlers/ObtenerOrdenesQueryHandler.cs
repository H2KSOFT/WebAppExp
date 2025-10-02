using AutoMapper;
using clApplication.DTOs;
using clDomain.Interfaces;
using MediatR;

namespace clApplication.Queries.Handlers
{
    public class ObtenerOrdenesQueryHandler : IRequestHandler<ObtenerOrdenesQuery, List<OrdenDto>>
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IMapper _mapper;

        public ObtenerOrdenesQueryHandler(IOrdenRepository ordenRepository, IMapper mapper)
        {
            _ordenRepository = ordenRepository;
            _mapper = mapper;
        }

        public async Task<List<OrdenDto>> Handle(ObtenerOrdenesQuery request, CancellationToken cancellationToken)
        {
            // Obtener todas las órdenes
            var ordenes = await _ordenRepository.ObtenerTodosAsync();

            // Aplicar filtros si se especifican
            if (!string.IsNullOrEmpty(request.Cliente))
            {
                ordenes = ordenes.Where(o => o.Cliente.Contains(request.Cliente, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (request.FechaDesde.HasValue)
            {
                ordenes = ordenes.Where(o => o.Fecha >= request.FechaDesde.Value).ToList();
            }

            if (request.FechaHasta.HasValue)
            {
                ordenes = ordenes.Where(o => o.Fecha <= request.FechaHasta.Value).ToList();
            }

            // Mapear a DTOs
            var ordenesDto = _mapper.Map<List<OrdenDto>>(ordenes);

            return ordenesDto;
        }
    }
}
