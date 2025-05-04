using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    internal class CreateMessageCommandHandler 
        (IYkotikaDbContext dbContext,
        IMapper mapper,
        INotificationService notificationService)
        : IRequestHandler<CreateMessageCommand, MessageItem>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly INotificationService _notificationService = notificationService;

        public async Task<MessageItem> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            if (request.Attachments == null && request.Text == null)
            {
                throw new Exception("Message couldn't be empty!");
            }

            var sender = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.SenderId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.SenderId);

            var chat = await
                _dbContext
                .Chats
                .FirstOrDefaultAsync(e => e.Id == request.ChatId, cancellationToken)
                ?? throw new NotFoundException(nameof(Chat), request.ChatId);

            var attachments = new List<Domain.Entities.File>();

            if (request.Attachments != null)
            {
                attachments = await
                    _dbContext
                    .Files
                    .Where(e => request.Attachments.Contains(e.Path))
                    .ToListAsync(cancellationToken);
            }

            var newMessage = new Message
            {
                Id = Guid.NewGuid(),
                Timestamps = new Timestamps(),
                Text = request.Text,
                Attachments = attachments,
                Sender = sender,
                Chat = chat
            };

            await _dbContext.Messages.AddAsync(newMessage, cancellationToken);

            foreach (var user in chat.Members) {

                if (user.Id == newMessage.Sender.Id)
                    continue;

                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    Timestamps = new Timestamps(),
                    Title = "Вам пришло новое сообщение",
                    Body = newMessage.Text ?? "...",
                    IsRead = false,
                    User = user,
                    UserId = user.Id
                };

                await _dbContext.Notifications.AddAsync(notification, cancellationToken);

                await _notificationService.Send(new NotifyDto(user.Id, _mapper.Map<NotificationItem>(notification)));
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MessageItem>(newMessage);
        }
    }
}
