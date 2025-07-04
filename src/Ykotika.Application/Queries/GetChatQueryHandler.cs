using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    internal class GetChatQueryHandler
        (IYkotikaDbContext dbContext, IMapper mapper) 
        : BaseGetQueryHandler(dbContext, mapper), 
        IRequestHandler<GetChatQuery, ChatDetails>
    {
        public async Task<ChatDetails> Handle(GetChatQuery request, CancellationToken cancellationToken)
        {
            var chat = await
                _dbContext
                .Chats
                .Include(e => e.Members)
                .Include(e => e.Messages)
                .ThenInclude(e => e.Sender)
                .Include(e => e.Messages)
                .ThenInclude(e => e.Attachments)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Chat), cancellationToken);

            chat.Messages = [.. chat.Messages.OrderBy(m => m.Timestamps.CreatedAt)];

            return _mapper.Map<ChatDetails>(chat);
        }
    }
}
