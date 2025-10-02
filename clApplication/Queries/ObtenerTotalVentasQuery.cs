using MediatR;

namespace clApplication.Queries
{
    public class ObtenerTotalVentasQuery : IRequest<decimal>
    {
        public DateTime Fecha { get; set; }
    }
}
