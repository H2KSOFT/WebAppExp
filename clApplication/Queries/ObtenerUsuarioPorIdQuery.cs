using clApplication.DTOs;
using MediatR;

namespace clApplication.Queries
{
    public class ObtenerUsuarioPorIdQuery : IRequest<UsuarioDto>
    {
        public int Id { get; set; }

        public ObtenerUsuarioPorIdQuery(int id)
        {
            Id = id;
        }
    }
}
