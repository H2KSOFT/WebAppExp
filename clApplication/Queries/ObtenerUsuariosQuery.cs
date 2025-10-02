using clApplication.DTOs;
using MediatR;

namespace clApplication.Queries
{
    public class ObtenerUsuariosQuery : IRequest<List<UsuarioDto>>
    {
    }
}
