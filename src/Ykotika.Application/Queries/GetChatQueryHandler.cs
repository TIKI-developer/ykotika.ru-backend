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
                .FirstOrDefaultAsync(e => e.Id == request.Id)
                ?? throw new NotFoundException(nameof(Chat), cancellationToken);

            return _mapper.Map<ChatDetails>(chat);
        }
    }
}
