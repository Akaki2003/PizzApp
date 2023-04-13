using FluentValidation;
using FluentValidation.Validators;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.API.Infrastructure.Localizations;

    namespace PizzApp.API.Infrastructure.Validators
{
    public class PizzaValidator : AbstractValidator<PizzaRequestModel>
    {
        public PizzaValidator()
        {
            RuleFor(pizza => pizza.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20)
                .WithMessage(nameof(PizzaRequestModel.Name) +"-"+ ErrorMessages.PizzaError );

            RuleFor(pizza => pizza.Price)
               .NotEmpty()
              .GreaterThan(0)
              .WithMessage(nameof(PizzaRequestModel.Price) + "-" + ErrorMessages.PizzaPrice);

            RuleFor(pizza => pizza.Description)
                .MaximumLength(100)
                .WithMessage(nameof(PizzaRequestModel.Price) + "-" + ErrorMessages.PizzaDescription);

            RuleFor(pizza => pizza.CaloryCount)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(nameof(PizzaRequestModel.CaloryCount) + "-" + ErrorMessages.PizzaCalory);
            ;

        }
    }
}
