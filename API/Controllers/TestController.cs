using Application.Features.Household.Queries.GetHouseholdById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<HouseholdModel> GetHouseholdById (int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetHouseholdByIdQuery
            {
                HouseholdId = id
            }, cancellationToken);
        }
    }
}
