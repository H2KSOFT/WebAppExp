using MediatR;

namespace clApplication.Commands
{
    public class EliminarOrdenCommand : IRequest
    {
        public Int64 Id { get; set; }
    }
}
