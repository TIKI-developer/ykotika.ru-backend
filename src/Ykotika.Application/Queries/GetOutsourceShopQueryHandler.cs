using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetOutsourceShopQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetOutsourceShopQuery, OutsourceShopDetails>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<OutsourceShopDetails>
            Handle(GetOutsourceShopQuery request, CancellationToken cancellationToken)
        {
            var outsourceShop = await
                _dbContext
                .OutsourceShops
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(OutsourceShop), request.Id);

            return _mapper.Map<OutsourceShopDetails>(outsourceShop);
        }
    }
}
