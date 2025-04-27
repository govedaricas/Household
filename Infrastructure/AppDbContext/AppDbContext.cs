using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Persistance.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AppDbContext
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Household> Households { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new HouseholdConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());
            base.OnModelCreating(builder);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
        }
    }
}
