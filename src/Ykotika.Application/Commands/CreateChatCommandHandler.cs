using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateChatCommandHandler 
        (IYkotikaDbContext dbContext)
        : IRequestHandler<CreateChatCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var members = await
                _dbContext
                .Users
                .Where(e => request.Members.Contains(e.Id))
                .ToListAsync(cancellationToken);

            var newChat = new Chat
            {
                Id = Guid.NewGuid(),
                Timestamps = new Timestamps(),
                Name = request.Name,
                Members = members
            };

            await _dbContext.Chats.AddAsync(newChat, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newChat.Id;
        }
    }
}
