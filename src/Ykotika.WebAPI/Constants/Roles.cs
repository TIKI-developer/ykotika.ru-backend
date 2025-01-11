using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Constants
{
    public static class Roles
    {
        public const string UNVERIFIED_ROLE  = nameof(UserRole.Unverified);
        public const string VERIFIED_ROLE = nameof(UserRole.Verified);
        public const string AUTHOR_ROLE = nameof(UserRole.Author);
        public const string ADMIN_ROLE = nameof(UserRole.Admin);
        public const string MODERATOR_ROLE = nameof(UserRole.Moderator);
        public const string DIRECTOR_ROLE = nameof(UserRole.Director);
    }
}
