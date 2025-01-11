using Microsoft.AspNetCore.Authorization;
using Ykotika.Domain.Interfaces;
using Ykotika.WebAPI.Authorization.Requirements;

namespace Ykotika.WebAPI.Authorization
{
    public class PublishedHandler
        : AuthorizationHandler<ContentRequirement, IPublishable>
    {
        protected override Task 
            HandleRequirementAsync
            (AuthorizationHandlerContext context, 
            ContentRequirement requirement, 
            IPublishable resource)
        {
            if (resource.IsPublished)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
