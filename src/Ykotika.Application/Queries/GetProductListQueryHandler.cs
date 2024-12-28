using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductListQueryHandler 
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetProductListQuery, ProductList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductList> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var products = await
                _dbContext
                .Products
                .Include(e => e.Images)
                .ProjectTo<ProductItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductList { Products = products };
        }
    }
}
