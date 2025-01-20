using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetOutsourceShopListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetOutsourceShopListQuery, BaseList<OutsourceShopItem>>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseList<OutsourceShopItem>>
            Handle(GetOutsourceShopListQuery request, CancellationToken cancellationToken)
        {
            var queryItems =
                _dbContext
                .OutsourceShops
                .Include(e => e.Image)
                .ProjectTo<OutsourceShopItem>(_mapper.ConfigurationProvider);

            return await BaseList<OutsourceShopItem>.CreateAsync(queryItems);
        }
    }
}
