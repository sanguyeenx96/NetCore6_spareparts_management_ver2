using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.System.User
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("First name is required")
                .MaximumLength(200).WithMessage("First name can not over 200 characters");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Mật khẩu phải chứa ít nhất 6 ký tự");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password is not match");
                }
            });
        }
    }
}