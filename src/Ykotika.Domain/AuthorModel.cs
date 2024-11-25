namespace Ykotika.Domain
{
    public class AuthorModel
    {
        public required Guid UserId { get; set; }
        public UserModel? User { get; set; }
        public required string Surname { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Socials { get; set; }
        public required bool ConfirmedOffer { get; set; }
        public required AuthorRequest Request { get; set; }
        public required AuthorStatus Status { get; set; }
    }
    public enum AuthorStatus
    {
        New,
        Confirmed
    }
}
