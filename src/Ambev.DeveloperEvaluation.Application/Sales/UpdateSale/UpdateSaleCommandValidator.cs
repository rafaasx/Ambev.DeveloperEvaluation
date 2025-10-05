using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.")
            .NotEqual(Guid.Empty).WithMessage("CustomerId must be a valid GUID.");

        RuleFor(x => x.SaleDate)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");

        RuleFor(x => x.Products)
             .NotEmpty().WithMessage("Products list cannot be empty.")
             .Must(p => p != null && p.Count > 0).WithMessage("Products list must contain at least one item.")
             .ForEach(product =>
             {
                 product.NotNull().WithMessage("Product cannot be null.");
                 product.SetValidator(new UpdateSaleItemCommandValidator());
             });
    }
}