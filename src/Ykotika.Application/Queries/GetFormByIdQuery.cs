using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFormByIdQuery : IRequest<FormDetails>
    {
        public required Guid Id { get; set; }
    }
}
