using clDomain.Entities;

namespace clDomain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Int64> CrearAsync(Usuario usuario);
        Task ActualizarAsync(Usuario usuario);
        Task EliminarAsync(Int64 id);

        Task<Usuario> ObtenerPorIdAsync(Int64 id);
        Task<Usuario> ObtenerPorNombreAsync(string nombre);
        Task<List<Usuario>> ObtenerTodosAsync();
        Task<List<Usuario>> ObtenerPorRolAsync(string rol);

        Task<Usuario> ValidarCredencialesAsync(string nombre, string clave);
        Task<bool> ExisteUsuarioAsync(string nombre);
    }
}
