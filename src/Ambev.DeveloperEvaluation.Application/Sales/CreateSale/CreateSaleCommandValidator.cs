using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.")
            .NotEqual(Guid.Empty).WithMessage("CustomerId must be a valid GUID.");

        RuleFor(x => x.SaleDate)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future.");

        RuleFor(x => x.Products)
             .NotEmpty().WithMessage("Products list cannot be empty.")
             .Must(p => p != null && p.Count > 0).WithMessage("Products list must contain at least one item.")
             .ForEach(product =>
             {
                 product.NotNull().WithMessage("Product cannot be null.");
                 product.SetValidator(new CreateSaleItemCommandValidator());
             });
    }
}