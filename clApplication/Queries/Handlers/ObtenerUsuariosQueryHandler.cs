using AutoMapper;
using clApplication.DTOs;
using clDomain.Interfaces;
using MediatR;

namespace clApplication.Queries.Handlers
{
    public class ObtenerUsuariosQueryHandler : IRequestHandler<ObtenerUsuariosQuery, List<UsuarioDto>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public ObtenerUsuariosQueryHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDto>> Handle(ObtenerUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.ObtenerTodosAsync();
            return _mapper.Map<List<UsuarioDto>>(usuarios);
        }
    }
}
