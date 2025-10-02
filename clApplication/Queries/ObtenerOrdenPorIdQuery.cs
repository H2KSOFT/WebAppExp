using clApplication.DTOs;
using MediatR;

namespace clApplication.Queries
{
    public class ObtenerOrdenPorIdQuery : IRequest<OrdenDto>
    {
        public int Id { get; set; }

        public ObtenerOrdenPorIdQuery(int id)
        {
            Id = id;
        }
    }
}
