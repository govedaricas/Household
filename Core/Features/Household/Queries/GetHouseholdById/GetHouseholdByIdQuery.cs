using MediatR;

namespace Application.Features.Household.Queries.GetHouseholdById
{
    public class GetHouseholdByIdQuery : IRequest<HouseholdModel>
    {
        public int HouseholdId { get; set; }
    }
}
