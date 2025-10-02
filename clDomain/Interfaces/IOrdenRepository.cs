using clDomain.Entities;

namespace clDomain.Interfaces
{
    public interface IOrdenRepository
    {
        Task<Int64> CrearAsync(Orden orden);
        Task ActualizarAsync(Orden orden);
        Task EliminarAsync(Int64 id);

        Task<Orden> ObtenerPorIdAsync(Int64 id);
        Task<List<Orden>> ObtenerTodosAsync();
        Task<List<Orden>> ObtenerPorClienteAsync(string cliente);

        Task<decimal> ObtenerTotalVentasPorFechaAsync(DateTime fecha);
        Task<bool> ExisteOrdenParaClienteAsync(string cliente, DateTime fecha);
    }
}
