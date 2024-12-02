using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Queries.GetById
{
    public class GetFormQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper) 
        : 
        IRequestHandler<GetFormQuery, FormViewModel>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<FormViewModel> Handle(GetFormQuery request, CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .Include(f => f.Inputs)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(FormModel), request.Id);

            return _mapper.Map<FormViewModel>(form);
        }
    }
}
