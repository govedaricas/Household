using Application.Interfaces;
using Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.RegisterUserCommand
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IPasswordService _passwordService;

        public RegisterUserCommandHandler(IAppDbContext dbContext, IPasswordService passwordService)
        {
            _dbContext = dbContext;
            _passwordService = passwordService;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _dbContext.Users.AnyAsync(x => x.Username == request.Username && x.IsActive, cancellationToken);
            if (userExists)
            {
                throw new Exception("User with provided username already exists.");
            }

            var permissions = await _dbContext.Permissions
                .Where(x => request.Permissions.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (permissions.Count != request.Permissions.Count)
            {
                throw new Exception("Permission not found.");
            }

            var passwordHash = _passwordService.HashPassword(request.Password, out byte[] passwordSalt);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Permissions = permissions,
                IsActive = true
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
