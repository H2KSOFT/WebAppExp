namespace clDomain.Entities
{
    public class Usuario
    {
        public Int64 Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public Int64 IdRol { get; set; }
        public Rol Rol { get; set; }
    }
}
