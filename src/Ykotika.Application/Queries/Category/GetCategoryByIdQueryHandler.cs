using MediatR;

namespace Ykotika.Application.Queries.Category
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery>
    {
        public Task Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
