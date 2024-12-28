using MediatR;

namespace Ykotika.Application.Commands
{
    public class GenerateProductSpreadsheetCommand : IRequest
    {
        public required Guid ProductId { get; set; }
    }
}
