using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateUserCommandHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(command.UserId, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found for update.", command.UserId);
                throw new NotFoundException("User", command.UserId);
            }

            _mapper.Map(command, user);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.PasswordHash);

            await _repository.UpdateUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User with ID {UserId} updated successfully.", user.Id);

            return user.Id;
        }
    }
}
