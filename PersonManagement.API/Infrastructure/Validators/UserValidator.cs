using FluentValidation;
using FluentValidation.Validators;
using PizzApp.Application.Users.Requests;
using PizzApp.Application.Users;
using PizzApp.API.Infrastructure.Localizations;

namespace PizzApp.API.Infrastructure.Validators
{
    public class UserValidator : AbstractValidator<UserRequestModel>
    {
        public UserValidator()
        {
          
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .WithMessage(nameof(UserRequestModel.FirstName) + "Firstname of user must be between 2-20 characters");

            RuleFor(user => user.LastName)
               .NotEmpty()
               .MinimumLength(2)
               .MaximumLength(30)
              .WithMessage(nameof(UserRequestModel.LastName) + "Lastname of user must be between 2 and 30 characters");

            RuleFor(user => user.Email)
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
                .WithMessage(nameof(UserRequestModel.Email) + "-" + ErrorMessages.EmailFormat);


            RuleFor(user => user.PhoneNumber)
                .Matches(@"^5\d{8}$")
                .WithMessage(nameof(UserRequestModel.PhoneNumber) + "-" + ErrorMessages.PhoneNumberFormat);

        }
    }
}
