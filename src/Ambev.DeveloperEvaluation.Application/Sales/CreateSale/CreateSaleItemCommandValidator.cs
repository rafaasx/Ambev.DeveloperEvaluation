using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    public CreateSaleItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.")
            .NotEqual(Guid.Empty).WithMessage("ProductId must be a valid GUID.");

        RuleFor(x => x.Quantity)
            .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");
    }
}
