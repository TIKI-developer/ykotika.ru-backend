using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class Author
    {
        public required Guid UserId { get; set; }
        public required User User { get; set; }
        public required Timestamps Timestamps { get; set; }
        public string? About { get; set; }
        public required List<Social> Socials { get; set; }
        public required AuthorRequest Request { get; set; }
        public required AuthorStatus Status { get; set; }
    }
    public enum AuthorStatus
    {
        New,
        Confirmed,
        Inactive,
        Rejected,
        Banned
    }
}
