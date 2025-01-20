namespace Ykotika.Domain.Entities
{
    public class User : Entity
    {
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Email { get; set; }
        public File? Image { get; set; }
        public required string PasswordHash { get; set; }
        public required bool ConfirmedPersonalDataProcessingPolicy { get; set; }
        public string? RefreshTokenHash { get; set; }
        public List<UserRole> Roles { get; set; } = [UserRole.Unverified];
        public bool IsEmailVerified => !Roles.Contains(UserRole.Unverified);
        public List<Agreement>? Agreements { get; set; }
    }

    public enum UserRole
    {
        Unverified,
        Verified,
        Author,
        Moderator,
        Director,
        Admin
    }
}
