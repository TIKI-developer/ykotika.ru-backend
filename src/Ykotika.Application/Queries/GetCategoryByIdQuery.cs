using MediatR;

namespace Ykotika.Application.Queries
{
    public class GetCategoryByIdQuery : IRequest
    {
        public required Guid Id { get; set; }
    }
}
