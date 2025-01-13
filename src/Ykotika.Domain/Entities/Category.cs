using Ykotika.Domain.Interfaces;

namespace Ykotika.Domain.Entities
{
    public class Category : Entity, IHasAuthor, IPublishable
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsPublished { get; set; }
        public required User User { get; set; }
        public File? Image { get; set; }
        public List<Product>? Products { get; set; }
    }
}
