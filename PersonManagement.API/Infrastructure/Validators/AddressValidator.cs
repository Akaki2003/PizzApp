using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Options;
using PizzApp.Application.Addresses.Requests;
using PizzApp.API.Infrastructure.Localizations;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.Persistence;
using System.Data.SqlClient;

namespace PizzApp.API.Infrastructure.Validators
{
    public class AddressValidator : AbstractValidator<AddressRequestModel>
    {
        public AddressValidator()
        {

            //check if userId and PizzaId exists in db and is not marked as deleted

            RuleFor(address => address.City)
                .NotEmpty()
                .WithMessage("City field must be filled")
                .MaximumLength(15)
                .WithMessage(nameof(AddressRequestModel.City) + "-" + ErrorMessages.CityError);

            RuleFor(address => address.Country)
               .NotEmpty()
                .WithMessage("Country field must be filled")
               .MaximumLength(15)
              .WithMessage(nameof(AddressRequestModel.Country) + "-" + ErrorMessages.CountryError);

            RuleFor(address => address.Region)
                .MaximumLength(15)
                .WithMessage(nameof(AddressRequestModel.Region) + "-" + ErrorMessages.RegionError);

            RuleFor(address => address.Description)
                .MaximumLength(100);



        }
    }
}
