namespace Ykotika.Domain
{
    public class AuthorRequest
    {
        public required string TellAboutYourself { get; set; }
        public required Social WhichSocial { get; set; }
    }
    public enum Social
    {
        Telegram,
        WhatsApp
    }
}
