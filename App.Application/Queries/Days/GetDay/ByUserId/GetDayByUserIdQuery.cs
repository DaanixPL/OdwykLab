using App.Application.Interfaces.Authorizable;
using OdwykLab.Domain.Entities;
using MediatR;

namespace App.Application.Queries.Days.GetDay.ByUserId
{
    public record GetDayByUserIdQuery(int userId) : IRequest<List<DateOnly>>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => userId;
        public bool AllowAdminOverride => false;
    }
}
