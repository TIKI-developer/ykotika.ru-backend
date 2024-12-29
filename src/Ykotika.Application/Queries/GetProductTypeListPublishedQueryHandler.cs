using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductTypeListPublishedQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetProductTypeListPublishedQuery, ProductTypeList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductTypeList>
            Handle(GetProductTypeListPublishedQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await
                _dbContext
                .ProductTypes
                .Where(e => e.IsPublished == true)
                .ProjectTo<ProductTypeItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductTypeList { ProductTypes = productTypes };
        }
    }
}
