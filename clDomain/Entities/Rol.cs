namespace clDomain.Entities
{
    public class Rol
    {
        public Int64 Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
