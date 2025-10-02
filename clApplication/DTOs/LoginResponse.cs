namespace clApplication.DTOs
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Rol { get; set; }
        public string Usuario { get; set; }
        public string Message { get; set; }
    }

    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
    }
}
