using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class Category : Entity
    {
        public required string Name { get; set; }
        public required Form Form { get; set; }
        public List<Product>? Products { get; set; }
    }
}
