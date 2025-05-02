using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetChatQuery : IRequest<ChatDetails>
    {
        public required Guid Id { get; set; }
    }
}
