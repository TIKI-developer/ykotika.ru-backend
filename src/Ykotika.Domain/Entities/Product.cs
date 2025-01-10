using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class Product : Entity
    {
        public required string Article { get; init; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsPublished { get; set; }
        public required ProductStatus Status { get; set; }
        public required List<Tag> Tags { get; set; }
        public List<string>? Comments { get; set; }
        public required File Source { get; set; }
        public required List<ImageListItem> Images { get; set; }
        public required List<OutsourceShopProductInfo> OutsourceShops { get; set; }
        public required FormRecord FormRecord { get; init; }
        public List<Category>? Categories { get; set; }
        public required ProductType ProductType { get; init; }
        public required Author Author { get; set; }
    }

    public enum ProductStatus
    {
        New,
        Moderating,
        Sell,
        Rejected
    }
}
