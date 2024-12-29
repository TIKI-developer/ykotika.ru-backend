using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFormListQuery : IRequest<FormList>
    {
        public bool? IsPublished { get; set; }
    }
}
