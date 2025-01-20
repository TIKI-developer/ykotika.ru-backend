using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetAuthorByUserQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetQueryHandler(dbContext, mapper),
        IRequestHandler<GetAuthorByUserQuery, AuthorDetails>
    {
        public async Task<AuthorDetails>
            Handle(GetAuthorByUserQuery request, CancellationToken cancellationToken)
        {
            var author = await
                _dbContext
                .Authors
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.UserId == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.Id);

            return _mapper.Map<AuthorDetails>(author);
        }
    }
}
