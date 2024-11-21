namespace Ykotika.Domain
{
    public class UserModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email {  get; set; }
        public required string PasswordHash { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsEmailVerified => Role != UserRole.Guest;
        public virtual UserRole Role => UserRole.Guest;

        public UserModel()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public void MarkUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public enum UserRole
    {
        Guest,
        Default
    }
}
