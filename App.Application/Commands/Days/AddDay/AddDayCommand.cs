using MediatR;

namespace App.Application.Commands.Days.AddDay
{
    public record AddDayCommand(int UserId, bool IsGood) : IRequest<Unit>;
}
