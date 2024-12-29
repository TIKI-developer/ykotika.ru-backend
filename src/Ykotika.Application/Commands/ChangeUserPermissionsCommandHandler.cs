using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class ChangeUserPermissionsCommandHandler 
        (IYkotikaDbContext dbContext)
        : IRequestHandler<ChangeUserPermissionsCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task 
            Handle(ChangeUserPermissionsCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.Id);

            user.Permissions = request.Permissions ?? user.Permissions;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
