using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateInputCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Label { get; set; }
        public bool? IsRequired { get; set; }
    }
}
