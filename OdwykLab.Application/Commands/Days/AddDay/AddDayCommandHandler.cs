using App.Domain.Abstractions;
using OdwykLab.Domain.Entities;
using MediatR;

namespace App.Application.Commands.Days.AddDay
{
    public class AddDayCommandHandler : IRequestHandler<AddDayCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddDayCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddDayCommand command, CancellationToken cancellationToken)
        {
            Day day = new Day
            {
                UserId = command.UserId,
                isGood = command.IsGood,
                Date = command.Date
            };

            await _unitOfWork.Days.AddDayAsync(day, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}