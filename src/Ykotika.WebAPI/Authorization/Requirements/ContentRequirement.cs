using Microsoft.AspNetCore.Authorization;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Authorization.Requirements
{
    public class ContentRequirement
        (UserRole[]? permanentAccessRoles,
        bool checkAuthor = true,
        bool checkPublished = true,
        bool checkRole = true)
        : IAuthorizationRequirement
    {
        public UserRole[] PermanentAccessRoles { get; set; } = permanentAccessRoles ?? [UserRole.Admin];
        public bool CheckAuthor { get; init; } = checkAuthor;
        public bool CheckPublished { get; init; } = checkPublished;
        public bool CheckRole { get; init; } = checkRole;
    }
}
