using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Constants
{
    public static class Roles
    {
        public const string GUEST_ROLE = nameof(UserPermission.Unverified);
        public const string AUTHOR_ROLE = nameof(UserPermission.Author);
        public const string CUSTOMER_ROLE = nameof(UserPermission.Customer);
    }
}
