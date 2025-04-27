using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetUserById
{
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IAppDbContext _dbContext;

        public GetUserByIdQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Where(x => x.Id == request.Id)
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                    IsActive = x.IsActive,
                    Permissions = x.Permissions.Select(p => new Item
                    {
                        Id = p.Id,
                        Name = p.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return user;
        }
    }
}
