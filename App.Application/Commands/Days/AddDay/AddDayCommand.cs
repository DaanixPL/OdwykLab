using MediatR;

namespace App.Application.Commands.Days.AddDay
{
    public record AddDayCommand(int UserId, bool IsGood, DateOnly Date) : IRequest<Unit>;
}
