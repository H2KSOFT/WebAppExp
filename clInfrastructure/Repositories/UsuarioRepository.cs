using clDomain.Entities;
using clDomain.Interfaces;
using clInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace clInfrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObtenerPorNombreAsync(string nombre)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Nombre == nombre);
        }

        public async Task<Usuario> ValidarCredencialesAsync(string nombre, string clave)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Nombre == nombre && u.Clave == clave);
        }

        public async Task<bool> ExisteUsuarioAsync(string nombre)
        {
            return await _context.Usuarios.AnyAsync(u => u.Nombre == nombre);
        }

        public async Task<Int64> CrearAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario.Id;
        }

        public async Task ActualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Int64 id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario> ObtenerPorIdAsync(Int64 id)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .ToListAsync();
        }

        public async Task<List<Usuario>> ObtenerPorRolAsync(string rol)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .Where(u => u.Rol.Nombre == rol)
                .ToListAsync();
        }
    }
}
