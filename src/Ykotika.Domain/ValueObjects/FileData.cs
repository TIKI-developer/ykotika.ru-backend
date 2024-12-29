namespace Ykotika.Domain.ValueObjects
{
    public class FileData
    {
        public required string Name { get; set; }
        public string? ContentType { get; set; }
        public required byte[] Content { get; set; }
    }
}
