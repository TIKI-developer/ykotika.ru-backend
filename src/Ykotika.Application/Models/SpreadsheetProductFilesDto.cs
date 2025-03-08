using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Models
{
    public class SpreadsheetProductFilesDto
    {
        public required string Article { get; set; }
        public required Domain.Entities.File Source { get; set; }
        public required List<ImageListItem> Images { get; set; }
    }
}
