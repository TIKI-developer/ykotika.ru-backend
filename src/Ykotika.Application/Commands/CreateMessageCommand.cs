using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class CreateMessageCommand : IRequest<MessageItem>
    {
        public string? Text { get; set; }
        public List<string>? Attachments { get; set; }
        public required Guid SenderId { get; set; }
        public required Guid ChatId { get; set; }
    }
}
