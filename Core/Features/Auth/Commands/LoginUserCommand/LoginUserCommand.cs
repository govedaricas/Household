using Application.Models;
using MediatR;

namespace Application.Features.Auth.Commands.LoginUserCommand
{
    public class LoginUserCommand : IRequest<LoginResponseModel>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
