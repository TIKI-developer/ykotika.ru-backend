using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetFormByIdQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetQueryHandler(dbContext, mapper),
        IRequestHandler<GetFormByIdQuery, FormDetails>
    {
        public async Task<FormDetails>
            Handle(GetFormByIdQuery request,
                   CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .Include(f => f.Inputs)
                .ThenInclude(e => e.ExtraAttributes)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Form), request.Id);

            form.Inputs = [.. form.Inputs.OrderBy(i => i.OrderIndex)];

            return _mapper.Map<FormDetails>(form);
        }
    }
}
