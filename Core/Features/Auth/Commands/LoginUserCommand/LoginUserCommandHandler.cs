using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.LoginUserCommand
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponseModel>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IPasswordService _passwordService;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUserCommandHandler(IJwtTokenService jwtTokenService, IPasswordService passwordService, IAppDbContext dbContext)
        {
            _jwtTokenService = jwtTokenService;
            _passwordService = passwordService;
            _dbContext = dbContext;
        }

        public async Task<LoginResponseModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var validPassword = _passwordService.VerifyPassword(user, request.Password);
            if (!validPassword)
            {
                throw new Exception("User password is not valid.");
            }

            return new LoginResponseModel
            {
                Flag = true,
                Message = "Login successfully!",
                Token = _jwtTokenService.GenerateJwtToken(user)
            };
        }
    }
}
