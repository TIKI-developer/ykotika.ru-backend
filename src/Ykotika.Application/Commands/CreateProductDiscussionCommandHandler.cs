using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    internal class CreateProductDiscussionCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<CreateProductDiscussionCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(CreateProductDiscussionCommand request, CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == request.ProductId, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.ProductId);

            var discussionCreator = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.CreatorId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.CreatorId);

            var members = new List<User>() { discussionCreator, product.User };

            var newChat = new Chat
            {
                Id = Guid.NewGuid(),
                Timestamps = new Domain.ValueObjects.Timestamps(),
                Members = members,
                Type = "productDiscussion"
            };

            product.Discussion = newChat;

            await _dbContext.Chats.AddAsync(newChat, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newChat.Id;
        }
    }
}
