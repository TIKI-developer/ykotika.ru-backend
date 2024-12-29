using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductTypeListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetProductTypeListQuery, ProductTypeList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductTypeList>
            Handle(GetProductTypeListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.ProductTypes.AsQueryable();

            if (request.IsPublished.HasValue)
            {
                query = query.Where(pt => pt.IsPublished == request.IsPublished.Value);
            }

            var productTypes = await query
                .ProjectTo<ProductTypeItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductTypeList { ProductTypes = productTypes };
        }
    }
}
