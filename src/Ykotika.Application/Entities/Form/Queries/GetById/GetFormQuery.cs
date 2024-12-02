using MediatR;

namespace Ykotika.Application.Entities.Form.Queries.GetById
{
    public class GetFormQuery : IRequest<FormViewModel>
    {
        public required Guid Id { get; set; }
    }
}
