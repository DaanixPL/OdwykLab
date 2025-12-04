using App.Application.Validators.ValidationMessages;
using FluentValidation;

namespace App.Application.Commands.Days.DeleteDay
{
    public class DeleteDayCommandValidator : AbstractValidator<DeleteDayCommand>
    {
        public DeleteDayCommandValidator() 
        {
            RuleFor(x => x.Date)
                 .Must(date => date != default)
                 .WithMessage(ValidationMessage.Required("Date"));
        }
    }
}
