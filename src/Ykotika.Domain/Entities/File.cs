using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class File
    {
        public required string Path { get; set; }
        public required Timestamps Timestamps { get; set; }
    }
}
