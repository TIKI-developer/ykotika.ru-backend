using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFileListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetFileListQuery, FileList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<FileList> Handle(GetFileListQuery request, CancellationToken cancellationToken)
        {
            var files = await
                _dbContext
                .Files
                .ProjectTo<FileItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new FileList { Files = files };
        }
    }
}
