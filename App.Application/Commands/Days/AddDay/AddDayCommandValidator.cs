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
                .RequiredField(nameof(AddDayCommand.UserId))
                .NoAlreadyExistsAsync(
                async (userId, cancellation) =>
                {
                    var day = await unitOfWork.Days.GetDaysByUserIdAsync(userId, cancellation);
                    return !day.Any(day => day.Date == DateOnly.FromDateTime(DateTime.UtcNow));
                },
                nameof(AddDayCommand.UserId)
                );
        }
    }
}
