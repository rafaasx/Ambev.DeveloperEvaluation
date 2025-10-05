using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
{
    public CreateRatingCommandValidator()
    {
        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(0).WithMessage("Rate cannot be negative.");

        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(0).WithMessage("Count cannot be negative.");
    }
}
