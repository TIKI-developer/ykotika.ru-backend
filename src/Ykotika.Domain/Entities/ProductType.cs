using Ykotika.Domain.Interfaces;

namespace Ykotika.Domain.Entities
{
    public class ProductType : Entity, IContent
    {
        public required string Name { get; set; }
        public required List<string> ArticlePattern { get; set; }
        public required bool IsPublished { get; set; }
        public string? ManualLink { get; set; }
        public required Form Form { get; set; }
        public List<Product>? Products { get; set; }
        public required User User { get; set; }
    }
}
