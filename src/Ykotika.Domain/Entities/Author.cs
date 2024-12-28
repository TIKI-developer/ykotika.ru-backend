using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class Author : Entity
    {
        public required string Socials { get; set; }
        public required AuthorRequest Request { get; set; }
        public required AuthorStatus Status { get; set; }
        public required User User { get; set; }
        public List<Agreement>? Agreements { get; set; }

        public void CreateRequest(string tellAboutYourself, Social whichSocial)
        {
            Request = new AuthorRequest()
            {
                TellAboutYourself = tellAboutYourself,
                WhichSocial = whichSocial,
                Timestamps = new Timestamps()
            };
        }
    }
    public enum AuthorStatus
    {
        New,
        Confirmed
    }
}
