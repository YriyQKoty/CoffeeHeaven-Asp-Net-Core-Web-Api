using FluentValidation;
using Library.Api.Requests;

namespace Library.Api.Validators
{
    public class OriginCountryRequestValidator : AbstractValidator<OriginCountryRequest>
    {
        public OriginCountryRequestValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).WithMessage("Provider ID cannot be negative");
            
            RuleFor(p => p.Name).MinimumLength(2).WithMessage("Name is too short");
            RuleFor(p => p.Name).MaximumLength(64).WithMessage("Name is too long");
            
        }
    }
}