using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetUserListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetUserListQuery, UserList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<UserList> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = await
                _dbContext
                .Users
                .Include(e => e.Image)
                .ProjectTo<UserItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new UserList { Users = users };
        }
    }
}
