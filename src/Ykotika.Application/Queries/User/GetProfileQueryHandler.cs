using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.User
{
    public class GetProfileQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<GetProfileQuery, ProfileViewModel>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ProfileViewModel> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
            }

            return _mapper.Map<ProfileViewModel>(user);
        }
    }
}
