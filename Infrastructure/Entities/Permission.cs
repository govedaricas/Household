using System.Collections.Generic;

namespace Infrastructure.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
