using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Household.Queries.GetHouseholdById
{
    public class GetHouseholdByIdQueryHandler : IRequestHandler<GetHouseholdByIdQuery, HouseholdModel>
    {
        private readonly IAppDbContext _dbContext;

        public GetHouseholdByIdQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<HouseholdModel> Handle(GetHouseholdByIdQuery request, CancellationToken cancellationToken)
        {
            var household = await _dbContext.Households
                .Select(x => new HouseholdModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    City = x.City,
                    CountryId = x.CountryId,
                    CreatingDate = x.CreatingDate,
                    Name = x.Name,
                    NumberOfUsers = x.NumberOfUsers,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync(cancellationToken);

            if (household == null)
            {
                throw new Exception("Household not found.");
            }

            return household;
        }
    }
}
