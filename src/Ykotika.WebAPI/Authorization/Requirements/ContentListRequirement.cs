using Microsoft.AspNetCore.Authorization;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Authorization.Requirements
{
    public class ContentListRequirement(UserRole[] roles) : IAuthorizationRequirement
    {
        public UserRole[] Roles { get; init; } = roles;
    }
}
