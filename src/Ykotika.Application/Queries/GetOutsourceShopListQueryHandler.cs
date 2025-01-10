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
        : IRequestHandler<GetOutsourceShopListQuery, OutsourceShopList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<OutsourceShopList>
            Handle(GetOutsourceShopListQuery request, CancellationToken cancellationToken)
        {
            var outsourceShops = await
                _dbContext
                .OutsourceShops
                .Include(e => e.Image)
                .ProjectTo<OutsourceShopItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new OutsourceShopList { OutsourceShops = outsourceShops };
        }
    }
}
