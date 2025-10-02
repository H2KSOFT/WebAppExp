using clApplication.Commands;
using FluentValidation;

namespace clApplication.Validators
{
    public class ActualizarOrdenCommandValidator : AbstractValidator<ActualizarOrdenCommand>
    {
        public ActualizarOrdenCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID de la orden debe ser mayor a 0");

            RuleFor(x => x.Cliente)
                .NotEmpty().WithMessage("El cliente es requerido")
                .MaximumLength(500).WithMessage("El cliente no puede tener más de 500 caracteres");

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha es requerida")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura");

            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("La orden debe tener al menos un detalle");

            RuleForEach(x => x.Detalles).ChildRules(detalle =>
            {
                detalle.RuleFor(d => d.Producto)
                    .NotEmpty().WithMessage("El producto es requerido")
                    .MaximumLength(500).WithMessage("El producto no puede tener más de 500 caracteres");

                detalle.RuleFor(d => d.Cantidad)
                    .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");

                detalle.RuleFor(d => d.PrecioUnitario)
                    .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a 0");

                detalle.RuleFor(d => d.SubTotal)
                    .GreaterThan(0).WithMessage("El subtotal debe ser mayor a 0")
                    .Must((detalle, subtotal) => subtotal == detalle.Cantidad * detalle.PrecioUnitario)
                    .WithMessage("El subtotal no coincide con la cantidad y precio unitario");
            });
        }
    }
}
