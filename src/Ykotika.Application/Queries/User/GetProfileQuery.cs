using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.User
{
    public class GetProfileQuery : IRequest<UserDetails>
    {
        public required Guid Id { get; set; }
    }
}
