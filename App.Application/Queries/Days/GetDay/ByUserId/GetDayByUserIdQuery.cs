using App.Application.Interfaces.Authorizable;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Queries.Days.GetDay.ByUserId
{
    public record GetDayByUserIdQuery(int userId) : IRequest<List<Day>>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => userId;
        public bool AllowAdminOverride => true;
    }
}
