using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
{
    public UpdateSaleItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.")
            .NotEqual(Guid.Empty).WithMessage("ProductId must be a valid GUID.");

        RuleFor(x => x.Quantity)
            .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");
    }
}
