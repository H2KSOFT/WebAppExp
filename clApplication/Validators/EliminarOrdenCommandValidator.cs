using clApplication.Commands;
using FluentValidation;

namespace clApplication.Validators
{
    public class EliminarOrdenCommandValidator : AbstractValidator<EliminarOrdenCommand>
    {
        public EliminarOrdenCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID de la orden debe ser mayor a 0");
        }
    }
}
