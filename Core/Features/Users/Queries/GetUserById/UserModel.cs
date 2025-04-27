using Application.Models;

namespace Application.Features.Users.Queries.GetUserById
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<Item>? Permissions { get; set; }
        public bool IsActive { get; set; }
    }
}
