using Ykotika.Domain;

namespace Ykotika.WebAPI.Constants
{
    public static class Roles
    {
        public const string GUEST_ROLE = nameof(UserRole.Guest);
        public const string AUTHOR_ROLE = nameof(UserRole.Author);
        public const string CUSTOMER_ROLE = nameof(UserRole.Customer);
    }
}
