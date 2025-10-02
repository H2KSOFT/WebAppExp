using clApplication.DTOs;
using clDomain.Entities;
using clDomain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace clApplication.Commands.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObtenerPorNombreAsync(request.Nombre);

            if (usuario == null || usuario.Clave != request.Clave)
            {
                return new LoginResponse { Success = false, Message = "Credenciales inválidas" };
            }

            var token = GenerateJwtToken(usuario);

            return new LoginResponse
            {
                Success = true,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2),
                Rol = usuario.Rol.Nombre,
                Usuario = usuario.Nombre
            };
        }

        private string GenerateJwtToken(Usuario usuario)
        {

            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new ArgumentNullException("Jwt:Key", "La clave JWT no está configurada");

            if (string.IsNullOrEmpty(jwtIssuer))
                throw new ArgumentNullException("Jwt:Issuer", "El emisor JWT no está configurado");

            if (string.IsNullOrEmpty(jwtAudience))
                throw new ArgumentNullException("Jwt:Audience", "La audiencia JWT no está configurada");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Role, usuario.Rol.Nombre),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
