using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

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

            product.Comments.Add(request.Content);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
