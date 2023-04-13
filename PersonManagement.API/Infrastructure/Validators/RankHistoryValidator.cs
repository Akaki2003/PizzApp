using FluentValidation;
using FluentValidation.Validators;
using PizzApp.Application.RankHistories.Requests;
using PizzApp.API.Infrastructure.Localizations;




namespace PizzApp.API.Infrastructure.Validators
{
    public class RankHistoryValidator : AbstractValidator<RankHistoryRequestModel>
    {
        public RankHistoryValidator()
        {
            //check if userId and PizzaId exists in db and is not marked as deleted
            RuleFor(rankHistory => rankHistory.Rank)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(10)
                .WithMessage(nameof(RankHistoryRequestModel.Rank) + "-" + ErrorMessages.RankAmount);

         
        }
    }
}
