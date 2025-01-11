using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Ykotika.WebAPI.Authorization.Requirements;

namespace Ykotika.WebAPI.Authorization
{
    public class RoleHandler : AuthorizationHandler<ContentRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ContentRequirement requirement)
        {
            var userRoles = context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            if (requirement.PermanentAccessRoles.Any(role => userRoles.Contains(role.ToString())))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}