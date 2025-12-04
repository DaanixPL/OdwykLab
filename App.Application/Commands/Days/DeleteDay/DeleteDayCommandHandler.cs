using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Commands.Days.DeleteDay
{
    public class DeleteDayCommandHandler : IRequestHandler<DeleteDayCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDayCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteDayCommand command, CancellationToken cancellationToken)
        {
            var days = await _unitOfWork.Days.GetDaysByUserIdAsync(command.UserId, cancellationToken);
            var dayToDelete = days.FirstOrDefault(d => d.Date == command.Date);

            if (dayToDelete == null)
            {
                throw new NotFoundException("Day", command.UserId);
            }

            await _unitOfWork.Days.DeleteDayAsync(dayToDelete, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}