using MediatR;

namespace Application.Features.Auth.Commands.RegisterUserCommand
{
    public class RegisterUserCommand : IRequest<int>
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public bool IsActive { get; set; }
        public List<int> Permissions { get; set; } = new List<int>();
    }
}
