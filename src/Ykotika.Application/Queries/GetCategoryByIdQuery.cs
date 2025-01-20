using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDetails>
    {
        public required Guid Id { get; set; }
    }
}
