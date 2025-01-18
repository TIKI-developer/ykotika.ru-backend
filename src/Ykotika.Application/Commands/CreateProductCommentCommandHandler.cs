using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateProductCommentCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<CreateProductCommentCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(CreateProductCommentCommand request, CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);

            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            product.Comments.Add
                (
                new Comment
                {
                    Author = user,
                    Content = request.Content,
                    CreatedAt = DateTime.UtcNow
                }
                );

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
