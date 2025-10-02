using clApplication.DTOs;
using MediatR;

namespace clApplication.Queries
{
    public class ObtenerOrdenesQuery : IRequest<List<OrdenDto>>
    {
        public string Cliente { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
