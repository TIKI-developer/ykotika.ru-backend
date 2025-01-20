using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Authorization.Requirements;

namespace Ykotika.WebAPI.Authorization
{
    public class AuthorHandler
        : AuthorizationHandler<ContentRequirement, IHasAuthor>
    {
        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context,
             ContentRequirement requirement,
             IHasAuthor resource)
        {
            if (!requirement.CheckAuthor)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Task.CompletedTask;
            }

            if (resource.UserId == userId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
