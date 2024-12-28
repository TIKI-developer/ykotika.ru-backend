namespace Ykotika.Domain.Entities
{
    public class File : Entity
    {
        public required string Name { get; set; }
        public required string RelativePath { get; set; }
    }
}
