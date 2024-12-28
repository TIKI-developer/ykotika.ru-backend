using MediatR;

namespace Ykotika.Application.Commands
{
    public class GenerateProductSpreadsheetCommand : IRequest<Guid>
    {
        public required List<Guid> Products { get; set; }
    }
}
