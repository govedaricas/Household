using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Enums;
using Infrastructure.AppDbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public class Authorize : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly PermissionEnum? _permission;

        public Authorize(PermissionEnum permission)
        {
            _permission = permission;
        }

        public Authorize()
        {
            _permission = null;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                context.Result = new ForbidResult(); // User not authenticated
                return;
            }

            int userId = int.Parse(userIdClaim.Value);

            if (_permission == null)
            {
                return;
            }

            // Access the DbContext (injected via dependency injection)
            var dbContext = context.HttpContext.RequestServices.GetService<AppDbContext>();
            if (dbContext == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            // Check if the user has the required permission in UserPermission
            bool hasPermission = dbContext.Users
                .Where(x => x.Id == userId)
                .Any(x => x.Permissions.Any(y => y.Id == (int)_permission));

            if (!hasPermission)
            {
                context.Result = new ForbidResult(); // User lacks permission
            }
        }
    }
}
