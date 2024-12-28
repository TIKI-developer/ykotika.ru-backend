using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class CreateFormCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public List<FormInputDto>? Inputs { get; set; }
    }
}
