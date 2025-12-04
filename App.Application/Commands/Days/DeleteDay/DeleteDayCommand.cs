using App.Application.Interfaces.Authorizable;
using MediatR;

namespace App.Application.Commands.Days.DeleteDay
{
    public record DeleteDayCommand(DateOnly Date, int UserId) : IRequest<Unit>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => UserId;

        public bool AllowAdminOverride => false;
    }
}
