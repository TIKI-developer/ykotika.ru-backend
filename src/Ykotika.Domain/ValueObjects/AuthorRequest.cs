namespace Ykotika.Domain.ValueObjects
{
    public class AuthorRequest
    {
        public required string TellAboutYourself { get; set; }
        public required ContactSocial WhichSocial { get; set; }
        public required Timestamps Timestamps { get; set; }
        public enum ContactSocial
        {
            Telegram,
            WhatsApp
        }
    }
}
