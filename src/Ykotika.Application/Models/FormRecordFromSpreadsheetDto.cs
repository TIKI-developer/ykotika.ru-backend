namespace Ykotika.Application.Models
{
    public class FormRecordFromSpreadsheetDto
    {
        public required List<InputRecordFromSpreadsheetDto> InputRecords { get; set; } = [];
        public required string UserEmail { get; set; }
        public bool IsPublished { get; set; }

        public class InputRecordFromSpreadsheetDto
        {
            public required string Name { get; set; }
            public required string Value { get; set; }
        }
    }
}
