using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Household> Households { get;  }
        DbSet<User> Users { get;  }
        DbSet<Permission> Permissions { get;  }

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
