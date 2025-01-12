using Microsoft.AspNetCore.Authorization;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Authorization.Requirements
{
    public class ContentListRequirement(UserRole[] permanentAccessRoles) : IAuthorizationRequirement
    {
        public UserRole[] PermanentAccessRoles { get; init; } = permanentAccessRoles;
    }
}
