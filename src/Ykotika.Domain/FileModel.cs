namespace Ykotika.Domain
{
    public class FileModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string RelativePath { get; set; }
    }
}
