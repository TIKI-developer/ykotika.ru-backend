using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFileListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetFileListQuery, PagedList<FileItem>>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedList<FileItem>> Handle(GetFileListQuery request, CancellationToken cancellationToken)
        {
            var queryItems =
                _dbContext
                .Files
                .ProjectTo<FileItem>(_mapper.ConfigurationProvider);

            return await PagedList<FileItem>.CreateAsync(queryItems, request.Pagination.Page, request.Pagination.PageSize);
        }
    }
}
