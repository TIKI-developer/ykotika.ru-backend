using AutoMapper;
using MediatR;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Commands.Create
{
    public class CreateFormCommandHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper) 
        : 
        IRequestHandler<CreateFormCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext; 
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            var form = new FormModel
            {
                Id = Guid.NewGuid(),
                Name = "Форма",
                Fields = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _dbContext.Forms.AddAsync(form, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return form.Id;
        }
    }
}
