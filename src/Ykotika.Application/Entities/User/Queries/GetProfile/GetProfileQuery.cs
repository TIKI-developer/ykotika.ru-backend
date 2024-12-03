using MediatR;

namespace Ykotika.Application.Entities.User.Queries.GetProfile
{
    public class GetProfileQuery : IRequest<ProfileViewModel>
    {
        public required Guid Id { get; set; }
    }
}
