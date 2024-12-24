namespace Ykotika.Domain.ValueObjects
{
    public class AuthorRequest
    {
        public required string TellAboutYourself { get; set; }
        public required Social WhichSocial { get; set; }
        public required Timestamps Timestamps { get; set; }
    }
    public enum Social
    {
        Telegram,
        WhatsApp
    }
}
