using Ykotika.Domain.Entities;

namespace Ykotika.Application.Models
{
    public class ProductFromSpreadsheetDto
    {
        public required string Article { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsPublished { get; set; }
        public required bool IsAdult { get; set; }
        public required ProductStatus Status { get; set; }
        public List<OutsourceShopFromSpreadsheetDto>? OutsourceShops { get; set; } = [];
        public required List<string> Tags { get; set; }
        public List<string> CategoryNames { get; set; } = [];
        public required string ProductTypeName { get; init; }
        public required string UserEmail { get; set; }
        public required FormRecordFromSpreadsheetDto FormRecord { get; set; }
        public required SpreadsheetProductFilesDto Files { get; set; }
    }
}
