using AutoMapper;
using clApplication.DTOs;
using clDomain.Interfaces;
using MediatR;

namespace clApplication.Queries.Handlers
{
    public class ObtenerUsuarioPorIdQueryHandler : IRequestHandler<ObtenerUsuarioPorIdQuery, UsuarioDto>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public ObtenerUsuarioPorIdQueryHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> Handle(ObtenerUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(request.Id);

            if (usuario == null)
                throw new KeyNotFoundException($"Usuario con ID {request.Id} no encontrado");

            return _mapper.Map<UsuarioDto>(usuario);
        }
    }
}
