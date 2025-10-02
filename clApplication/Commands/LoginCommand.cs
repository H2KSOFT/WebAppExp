using clApplication.DTOs;
using MediatR;

namespace clApplication.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Nombre { get; set; }
        public string Clave { get; set; }
    }
}
