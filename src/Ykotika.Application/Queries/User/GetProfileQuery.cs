using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.User
{
    public class GetProfileQuery : IRequest<ProfileViewModel>
    {
        public required Guid Id { get; set; }
    }
}
