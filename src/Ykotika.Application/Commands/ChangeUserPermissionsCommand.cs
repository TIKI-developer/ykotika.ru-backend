using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class ChangeUserPermissionsCommand : IRequest
    {
        public required Guid Id { get; set; }
        public List<UserPermission>? Permissions { get; set; }
    }
}
