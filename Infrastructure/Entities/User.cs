using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.Entities
{
    public class User 
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
