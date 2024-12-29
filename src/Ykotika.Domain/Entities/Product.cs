using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class Product : Entity
    {
        public required string Article { get; init; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsPublished { get; set; }
        public List<OutsourceShop>? OutsourceShops { get; set; }
        public List<ImageListItem>? Images { get; set; }
        public required FormRecord FormRecord { get; init; }
        public required ProductType ProductType { get; init; }
    }
}
