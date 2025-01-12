using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetOutsourceShopByIdQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetQueryHandler(dbContext, mapper),
        IRequestHandler<GetOutsourceShopByIdQuery, OutsourceShopDetails>
    {
        public async Task<OutsourceShopDetails>
            Handle(GetOutsourceShopByIdQuery request, CancellationToken cancellationToken)
        {
            var outsourceShop = await
                _dbContext
                .OutsourceShops
                .Include(e => e.Image)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(OutsourceShop), request.Id);

            return _mapper.Map<OutsourceShopDetails>(outsourceShop);
        }
    }
}
