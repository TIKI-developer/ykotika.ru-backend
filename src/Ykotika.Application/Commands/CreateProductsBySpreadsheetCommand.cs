using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateProductsBySpreadsheetCommand : IRequest
    {
        public required string SpreadsheetFilePath { get; set; }
        public required string ZipFilePath { get; set; }
    }
}
