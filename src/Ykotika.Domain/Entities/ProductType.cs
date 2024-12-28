namespace Ykotika.Domain.Entities
{
    public class ProductType : Entity
    {
        public required string Name { get; set; }
        public required Form Form { get; set; }
        public required bool IsPublished { get; set; }
        public List<Product>? Products { get; set; }
    }
}
