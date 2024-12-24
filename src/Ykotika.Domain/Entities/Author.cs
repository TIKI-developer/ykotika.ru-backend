using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class Author : Entity
    {
        public required string Socials { get; set; }
        public required AuthorRequest Request { get; set; }
        public required AuthorStatus Status { get; set; }
        public List<Agreement>? Agreements { get; set; }
        public List<FormRecord>? SubmittedForms { get; set; }
    }
    public enum AuthorStatus
    {
        New,
        Confirmed
    }
}
