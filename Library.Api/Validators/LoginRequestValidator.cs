using FluentValidation;
using Library.Api.Requests;

namespace Library.Api.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
     
        public LoginRequestValidator()
        {
            
            RuleFor(l => l.UserName).NotEmpty();

            RuleFor(l => l.Password).NotEmpty();
        }
    }
}