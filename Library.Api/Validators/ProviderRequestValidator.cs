using System.Data;
using FluentValidation;
using Library.Api.Requests;

namespace Library.Api.Validators
{
    public class ProviderRequestValidator : AbstractValidator<ProviderRequest>
    {
        public ProviderRequestValidator()
        {
            
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Name is too short");
            RuleFor(p => p.Name).MaximumLength(64).WithMessage("Name is too long");
            
            RuleFor(p => p.OriginCountryId).GreaterThan(-1).WithMessage("Origin Country ID cannot be negative");
        }
    }
}