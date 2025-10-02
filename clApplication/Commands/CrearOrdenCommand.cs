using clApplication.DTOs;
using MediatR;

namespace clApplication.Commands
{
    public class CrearOrdenCommand : IRequest<Int64>
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public List<DetalleOrdenDto> Detalles { get; set; } = new List<DetalleOrdenDto>();
    }
}
