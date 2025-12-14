using App.Application.Validators.Extensions;
using App.Domain.Abstractions;
using FluentValidation;

namespace App.Application.Commands.Days.AddDay
{
    public class AddDayCommandValidator : AbstractValidator<AddDayCommand>
    {
        public AddDayCommandValidator(IUnitOfWork unitOfWork) 
        {

            RuleFor(x => x.UserId)
                .RequiredField(nameof(AddDayCommand.UserId));

            RuleFor(x => x.Date)
            .MustAsync(async (command, date, cancellation) =>
            {
                var days = await unitOfWork.Days.GetDaysByUserIdAsync(command.UserId, cancellation);
                Console.WriteLine($"Validating date {date} for user {command.UserId}, existing days count: {days.Count()}");
                return !days.Any(d => d.Date == date);
            })
            .WithMessage("Day already exists for this user.");
        }
    }
}
