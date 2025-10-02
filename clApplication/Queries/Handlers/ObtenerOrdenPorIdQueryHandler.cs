using AutoMapper;
using clApplication.DTOs;
using clDomain.Interfaces;
using MediatR;

namespace clApplication.Queries.Handlers
{
    public class ObtenerOrdenPorIdQueryHandler : IRequestHandler<ObtenerOrdenPorIdQuery, OrdenDto>
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IMapper _mapper;

        public ObtenerOrdenPorIdQueryHandler(IOrdenRepository ordenRepository, IMapper mapper)
        {
            _ordenRepository = ordenRepository;
            _mapper = mapper;
        }

        public async Task<OrdenDto> Handle(ObtenerOrdenPorIdQuery request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepository.ObtenerPorIdAsync(request.Id);

            if (orden == null)
            {
                throw new KeyNotFoundException($"Orden con ID {request.Id} no encontrada");
            }

            return _mapper.Map<OrdenDto>(orden);
        }
    }
}
