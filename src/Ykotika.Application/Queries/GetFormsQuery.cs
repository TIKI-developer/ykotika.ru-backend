using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFormsQuery : IRequest<FormList> { }
}
