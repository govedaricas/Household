using System;

namespace Infrastructure.Entities
{
    public class Household
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public required string Address { get; set; }
        public int CountryId { get; set; }
        public required string City { get; set; }
        public int NumberOfUsers { get; set; }
        public required DateOnly CreatingDate { get; set; }
        public bool IsActive { get; set; }
    }
}
