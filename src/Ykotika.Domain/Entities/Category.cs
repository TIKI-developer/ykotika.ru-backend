using Ykotika.Domain.Interfaces;

namespace Ykotika.Domain.Entities
{
    public class Category : Entity, IContent
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required File Image { get; set; }
        public List<Product>? Products { get; set; }
        public required bool IsPublished { get; set; }
        public required User Author { get; set; }
    }
}
