using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.Form
{
    public class GetFormQuery : IRequest<FormViewModel>
    {
        public required Guid Id { get; set; }
    }
}
