using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

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
        public List<OutsourceShopFromSpreadsheet>? OutsourceShops { get; set; } = [];
        public required List<string> Tags { get; set; }
        public List<string> CategoryNames { get; set; } = [];
        public required string ProductTypeName { get; init; }
        public required string UserEmail { get; set; }
        public required FormRecordFromSpreadsheet FormRecord {  get; set; }
        public required FilesFromZipForSpreadsheet Files { get; set; }

        public class FormRecordFromSpreadsheet
        {
            public required List<InputRecordFromSpreadsheet> InputRecords { get; set; } = [];
            public required string UserEmail { get; set; }
            public bool IsPublished { get; set; }

            public class InputRecordFromSpreadsheet
            {
                public required string Name { get; set; }
                public required string Value { get; set; }
            }
        }
        public class OutsourceShopFromSpreadsheet
        {
            public required string Name { get; set; }
            public required string Link { get; set; }
        }
    }
    public class FilesFromZipForSpreadsheet
    {
        public required string Article { get; set; }
        public required Domain.Entities.File Source { get; set; }
        public required List<ImageListItem> Images { get; set; }
    }
}
