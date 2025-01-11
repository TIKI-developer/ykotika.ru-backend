using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Ykotika.WebAPI.Authorization.Requirements;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Authorization
{
    public class ContentListHandler
        : AuthorizationHandler<ContentListRequirement, ContentResourceDto>
    {
        protected override
            Task HandleRequirementAsync
            (AuthorizationHandlerContext context,
            ContentListRequirement requirement,
            ContentResourceDto resource)
        {
            if (resource.IsPublished.HasValue && resource.IsPublished.Value)
            {
                context.Succeed(requirement);
            }
            else
            {
                var userRoles = context.User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value);

                if (requirement.Roles.Any(role => userRoles.Contains(role.ToString())))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
