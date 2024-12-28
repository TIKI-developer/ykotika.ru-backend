using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class AddInputCommand : IRequest<Guid>
    {
        public Guid FormId { get; set; }
        public required int OrderIndex { get; set; }
        public required string Label { get; set; }
        public InputType Type { get; set; }
        public bool IsRequired { get; set; }
    }
}
