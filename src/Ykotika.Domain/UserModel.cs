namespace Ykotika.Domain
{
    public class UserModel
    {
        public required Guid Id { get; init; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required bool ConfirmedPolicy { get; set; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        public List<UserRole> Roles { get; set; } = [UserRole.Guest];
        public bool IsEmailVerified => !Roles.Contains(UserRole.Guest);

        public void MarkUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public enum UserRole
    {
        Guest,
        Customer,
        Author
    }
}
