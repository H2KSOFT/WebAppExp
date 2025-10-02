using clDomain.Entities;
using clDomain.Interfaces;
using MediatR;

namespace clApplication.Commands.Handlers
{
    public class CrearOrdenCommandHandler : IRequestHandler<CrearOrdenCommand, Int64>
    {
        private readonly IOrdenRepository _ordenRepository;

        public CrearOrdenCommandHandler(IOrdenRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        public async Task<Int64> Handle(CrearOrdenCommand request, CancellationToken cancellationToken)
        {
            // Validar que haya detalles
            if (request.Detalles == null || !request.Detalles.Any())
            {
                throw new ArgumentException("La orden debe tener al menos un detalle");
            }

            // Calcular total
            var total = request.Detalles.Sum(d => d.SubTotal);

            // Crear entidad Orden
            var orden = new Orden
            {
                Fecha = request.Fecha,
                Cliente = request.Cliente,
                Total = total,
                Detalles = request.Detalles.Select(d => new DetalleOrden
                {
                    Producto = d.Producto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    SubTotal = d.SubTotal
                }).ToList()
            };

            // Guardar en base de datos
            var ordenId = await _ordenRepository.CrearAsync(orden);

            return ordenId;
        }
    }
}
