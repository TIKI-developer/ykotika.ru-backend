namespace Ykotika.Domain.Entities
{
    public class Product : Entity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<OutsourceShop>? OutsourceShops { get; set; }
        public List<File>? Images { get; set; }
        public required bool IsPublished { get; set; }
        public required FormRecord FormRecord { get; init; }
        public required ProductType ProductType { get; init; }
    }
}
