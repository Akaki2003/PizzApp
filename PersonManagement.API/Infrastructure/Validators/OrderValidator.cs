using FluentValidation;
using FluentValidation.Validators;
using PizzApp.API.Infrastructure.Localizations;
using PizzApp.Application.Orders.Requests;



namespace PizzApp.API.Infrastructure.Validators
{
    public class OrderValidator : AbstractValidator<OrderRequestModel>
    {
        public OrderValidator()
        {
            //RuleFor(order => order.UserId)  check if userId and PizzaId exists in db and is not marked as deleted
            //    .Must(id =>
            //    {
            //        using(var db = )
            //    })

            RuleFor(order => order.PizzaId)
            .NotEmpty() 
            .WithMessage(ErrorMessages.PizzaExist);

        }
    }
}
