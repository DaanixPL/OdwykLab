using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Queries.Days.GetDay.ByUserId
{
    public class GetDayByUserIdHandler : IRequestHandler<GetDayByUserIdQuery, List<Day>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDayByUserIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Day>> Handle(GetDayByUserIdQuery query, CancellationToken cancellationToken)
        {
            var days = await _unitOfWork.Days.GetDaysByUserIdAsync(query.userId, cancellationToken);

            if (!days.Any())
            {
                throw new NotFoundException("Days", query.userId);
            }

            return days;
        }
    }
}
