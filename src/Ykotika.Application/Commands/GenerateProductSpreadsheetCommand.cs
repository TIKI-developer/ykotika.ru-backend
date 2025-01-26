using MediatR;

namespace Ykotika.Application.Commands
{
    public class GenerateProductSpreadsheetCommand : IRequest<string>
    {
        public required List<Guid> Products { get; set; }
        public required string RootUrl { get; set; }
    }
}
