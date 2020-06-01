using FluentValidation;
using Library.Api.Requests;

namespace Library.Api.Validators
{
    public class CoffeeRequestValidator : AbstractValidator<CoffeeRequest>
    {
        public CoffeeRequestValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Coffee ID cannot be negative");
            
            RuleFor(c => c.Name).MinimumLength(3).WithMessage("Name is too short");
            RuleFor(c => c.Name).MaximumLength(64).WithMessage("Name is too long");

            RuleFor(c => c.Price).InclusiveBetween(25, 5000).WithMessage("Price should be in range 25-5000!");

            RuleFor(c => c.Description).MinimumLength(36)
                .WithMessage("Description should be greater than 36 characters");
            
            RuleFor(c => c.Description).MaximumLength(255)
                .WithMessage("Description should be not greater than 255 characters");

            RuleFor(c => c.ProviderId).GreaterThan(0).WithMessage("Provider ID cannot be negative");

        }
    }
}