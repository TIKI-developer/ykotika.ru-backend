using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateUserRolesCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateUserRolesCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task
            Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.Id);

            user.Roles = request.Roles ?? user.Roles;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
