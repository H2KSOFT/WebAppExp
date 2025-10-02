using clApplication.DTOs;
using MediatR;

namespace clApplication.Commands
{
    public class ActualizarOrdenCommand : IRequest
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public List<DetalleOrdenDto> Detalles { get; set; } = new List<DetalleOrdenDto>();
    }
}
