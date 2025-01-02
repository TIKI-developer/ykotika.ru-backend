using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAuthorByUserQuery : IRequest<AuthorDetails>
    {
        public required Guid Id { get; set; }
    }
}
