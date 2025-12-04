using MediatR;

namespace App.Application.Commands.TokenRefreshRequest.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(string RefreshToken) : IRequest<Unit>;
}
