using clDomain.Entities;
using clDomain.Interfaces;
using MediatR;

namespace clApplication.Commands.Handlers
{
    public class ActualizarOrdenCommandHandler : IRequestHandler<ActualizarOrdenCommand>
    {
        private readonly IOrdenRepository _ordenRepository;

        public ActualizarOrdenCommandHandler(IOrdenRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        public async Task Handle(ActualizarOrdenCommand request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepository.ObtenerPorIdAsync(request.Id);

            if (orden == null)
                throw new KeyNotFoundException($"Orden con ID {request.Id} no encontrada");

            // Actualizar propiedades
            orden.Fecha = request.Fecha;
            orden.Cliente = request.Cliente;
            orden.Total = request.Detalles.Sum(d => d.SubTotal);

            // Actualizar detalles (eliminar existentes y agregar nuevos)
            orden.Detalles.Clear();
            foreach (var detalleDto in request.Detalles)
            {
                orden.Detalles.Add(new DetalleOrden
                {
                    Producto = detalleDto.Producto,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitario = detalleDto.PrecioUnitario,
                    SubTotal = detalleDto.SubTotal
                });
            }

            await _ordenRepository.ActualizarAsync(orden);
        }
    }
}
