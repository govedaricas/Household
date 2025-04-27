using API.Extensions;
using Application.Enums;
using Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [Authorize(PermissionEnum.UserManage)]
        public Task<UserModel> GetUserById(int id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetUserByIdQuery
            {
                Id = id
            }, cancellationToken);
        }
    }
}
