using MediatR;

namespace Ykotika.Application.Queries.Category
{
    public class GetCategoryByIdQuery : IRequest
    {
        public required Guid Id { get; set; }
    }
}
