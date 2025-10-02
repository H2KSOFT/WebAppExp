using clDomain.Entities;
using clDomain.Interfaces;
using clInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace clInfrastructure.Repositories
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Int64> CrearAsync(Orden orden)
        {
            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();
            return orden.Id;
        }

        public async Task ActualizarAsync(Orden orden)
        {
            _context.Ordenes.Update(orden);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Int64 id)
        {
            var orden = await _context.Ordenes.FindAsync(id);
            if (orden != null)
            {
                _context.Ordenes.Remove(orden);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Orden> ObtenerPorIdAsync(Int64 id)
        {
            return await _context.Ordenes
                .Include(o => o.Detalles)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Orden>> ObtenerTodosAsync()
        {
            return await _context.Ordenes
                .Include(o => o.Detalles)
                .OrderByDescending(o => o.Fecha)
                .ToListAsync();
        }

        public async Task<List<Orden>> ObtenerPorClienteAsync(string cliente)
        {
            return await _context.Ordenes
                .Include(o => o.Detalles)
                .Where(o => o.Cliente.Contains(cliente))
                .ToListAsync();
        }

        public async Task<decimal> ObtenerTotalVentasPorFechaAsync(DateTime fecha)
        {
            return await _context.Ordenes
                .Where(o => o.Fecha.Date == fecha.Date)
                .SumAsync(o => o.Total);
        }

        public async Task<bool> ExisteOrdenParaClienteAsync(string cliente, DateTime fecha)
        {
            return await _context.Ordenes
                .AnyAsync(o => o.Cliente == cliente && o.Fecha.Date == fecha.Date);
        }
    }
}
