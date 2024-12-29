using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetUserByIdQuery : IRequest<UserDetails>
    {
        public required Guid Id { get; set; }
    }
}
