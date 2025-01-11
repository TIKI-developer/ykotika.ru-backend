using Microsoft.AspNetCore.Authorization;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Authorization.Requirements
{
    public class ContentRequirement(UserRole[] permanentAccessRoles) : IAuthorizationRequirement
    {
        public UserRole[] PermanentAccessRoles { get; set; } = permanentAccessRoles;
    }
}
