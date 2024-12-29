using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Constants
{
    public static class Roles
    {
        public const string GUEST_ROLE = nameof(UserPermission.Unverified);
        public const string AUTHOR_ROLE = nameof(UserPermission.Author);
        public const string CUSTOMER_ROLE = nameof(UserPermission.Customer);
        public const string ADMIN_ROLE = nameof(UserPermission.Admin);
        public const string MODERATOR_ROLE = nameof(UserPermission.Moderator);
        public const string DIRECTOR_ROLE = nameof(UserPermission.Director);
    }
}
