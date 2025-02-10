using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Ykotika.Domain.Entities;
using Ykotika.WebAPI.Authorization.Requirements;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Authorization
{
    public class ProductStatusHandler
        : AuthorizationHandler<ProductStatusRequirement, UpdateProductStatusAuthorizationDto>
    {
        protected override Task
            HandleRequirementAsync
            (AuthorizationHandlerContext context,
            ProductStatusRequirement requirement,
            UpdateProductStatusAuthorizationDto resource)
        {
            var userRoles = context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            switch (resource.To)
            {
                case ProductStatus.Edit:
                case ProductStatus.PendingModeration:
                case ProductStatus.Fixed:
                    if (userRoles.Contains(UserRole.Author.ToString()) ||
                        userRoles.Contains(UserRole.Moderator.ToString()) ||
                        userRoles.Contains(UserRole.Admin.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case ProductStatus.Moderating:
                case ProductStatus.Done:
                case ProductStatus.Selling:
                case ProductStatus.Incorrect:
                    if (userRoles.Contains(UserRole.Moderator.ToString()) ||
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
