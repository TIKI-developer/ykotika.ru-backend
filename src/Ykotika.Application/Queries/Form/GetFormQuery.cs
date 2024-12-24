using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.Form
{
    public class GetFormQuery : IRequest<FormDetails>
    {
        public required Guid Id { get; set; }
    }
}
