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
            var inputs = _mapper.Map<List<FormInputModel>>(request.Inputs);
            var form = new FormModel
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Inputs = inputs,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            foreach (var input in inputs)
            {
                input.Id = Guid.NewGuid();
                input.Form = form;
                await _dbContext.FormInputs.AddAsync(input, cancellationToken);
            }

            await _dbContext.Forms.AddAsync(form, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return form.Id;
        }
    }
}
