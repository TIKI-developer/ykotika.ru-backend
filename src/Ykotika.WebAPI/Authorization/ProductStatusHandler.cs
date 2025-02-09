using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Ykotika.Domain.Entities;
using Ykotika.WebAPI.Authorization.Requirements;

namespace Ykotika.WebAPI.Authorization
{
    public class ProductStatusHandler
        : AuthorizationHandler<ProductStatusRequirement, ProductStatus>
    {
        protected override Task 
            HandleRequirementAsync
            (AuthorizationHandlerContext context, 
            ProductStatusRequirement requirement,
            ProductStatus resource)
        {
            var userRoles = context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            switch (resource)
            {
                case ProductStatus.Edit:
                    if (userRoles.Contains(UserRole.Author.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case ProductStatus.PendingModeration:
                    if (userRoles.Contains(UserRole.Author.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case ProductStatus.Moderating:
                    if (userRoles.Contains(UserRole.Moderator.ToString()) ||
                        userRoles.Contains(UserRole.Admin.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case ProductStatus.Done:
                    if (userRoles.Contains(UserRole.Moderator.ToString()) ||
                        userRoles.Contains(UserRole.Admin.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case ProductStatus.Selling:
                    if (userRoles.Contains(UserRole.Moderator.ToString()) ||
                        userRoles.Contains(UserRole.Admin.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case ProductStatus.Incorrect:
                    if (userRoles.Contains(UserRole.Moderator.ToString()) ||
                        userRoles.Contains(UserRole.Admin.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case ProductStatus.Fixed:
                    if (userRoles.Contains(UserRole.Author.ToString()) ||
                        userRoles.Contains(UserRole.Moderator.ToString()) ||
                        userRoles.Contains(UserRole.Admin.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                    break;
            }
            return Task.CompletedTask;
        }
    }
}
