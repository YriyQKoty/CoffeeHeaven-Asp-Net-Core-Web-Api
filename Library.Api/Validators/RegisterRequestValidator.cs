using System.Data;
using FluentValidation;
using Library.Api.Requests;

namespace Library.Api.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.UserName).MaximumLength(255).WithMessage("Username is too long");
            RuleFor(r => r.UserName).MinimumLength(4).WithMessage("Username is too short");
            
            RuleFor(r => r.Email).EmailAddress().WithMessage("Enter valid email");
            
            RuleFor(r => r.PhoneNumber).MaximumLength(10).WithMessage("Phone number cannot be greater than 10 digits");
            RuleFor(r => r.PhoneNumber).MinimumLength(8).WithMessage("Phone number cannot be less than 8 digits");

            RuleFor(r => r.Password).MinimumLength(8).WithMessage("Password should have at lest 8 characters");
            RuleFor(r => r.Password).MaximumLength(127).WithMessage("Password cannot be greater than 127 characters");

            RuleFor(r => r.PasswordConfirm).Equal(r => r.Password).WithMessage("Your passwords should match.");
            RuleFor(r => r.PasswordConfirm).MinimumLength(8).WithMessage("Password should have at lest 8 characters");
            RuleFor(r => r.PasswordConfirm).MaximumLength(127).WithMessage("Password cannot be greater than 127 characters");
            
        }
    }
}