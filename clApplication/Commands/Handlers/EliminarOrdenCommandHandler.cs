using clDomain.Interfaces;
using MediatR;

namespace clApplication.Commands.Handlers
{
    public class EliminarOrdenCommandHandler : IRequestHandler<EliminarOrdenCommand>
    {
        private readonly IOrdenRepository _ordenRepository;

        public EliminarOrdenCommandHandler(IOrdenRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        public async Task Handle(EliminarOrdenCommand request, CancellationToken cancellationToken)
        {
            await _ordenRepository.EliminarAsync(request.Id);
        }
    }
}
