using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateUserRolesCommand : IRequest
    {
        public required Guid Id { get; set; }
        public List<UserRole>? Roles { get; set; }
    }
}
