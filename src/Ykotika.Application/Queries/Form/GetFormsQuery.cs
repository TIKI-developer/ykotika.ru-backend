using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.Form
{
    public class GetFormsQuery : IRequest<FormList> { }
}
