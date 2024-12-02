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
            request.Fields.ForEach(f => Console.WriteLine(f));

            var fields = _mapper.Map<List<FormInputModel>>(request.Fields);

            var form = new FormModel
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Fields = fields,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            foreach (var field in form.Fields) 
            { 
                field.Id = Guid.NewGuid();
                field.Form = form;
                await _dbContext.FormInputs.AddAsync(field, cancellationToken);
            }

            await _dbContext.Forms.AddAsync(form, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return form.Id;
        }
    }
}
