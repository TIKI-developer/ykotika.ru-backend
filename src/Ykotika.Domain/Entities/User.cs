using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class User : Entity
    {
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Email { get; set; }
        public File? Avatar { get; set; }
        public required string PasswordHash { get; set; }
        public required bool ConfirmedPolicy { get; set; }
        public List<UserPermission> Permissions { get; set; } = [UserPermission.Unverified];
        public bool IsEmailVerified => !Permissions.Contains(UserPermission.Unverified);
    }

    public enum UserPermission
    {
        Unverified,
        Customer,
        Author,
        Moderator,
        Director,
        Admin
    }
}
