using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateAuthorCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task
            Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await
                _dbContext
                .Authors
                .FirstOrDefaultAsync(e => e.UserId == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Author), request.Id);

            author.About = request.About ?? author.About;
            author.Socials = request.Socials ?? author.Socials;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
