using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text.Json;
using Ykotika.Application.Interfaces;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Hubs
{
    public interface INotificationClient
    {
        Task ReceiveNotificationList(PagedList<NotificationItem> notifications);
        Task ReceiveNotification(NotificationItem notification);
    }

    public class NotificationHub : Hub<INotificationClient>, IDisposable
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly INotificationSender _notificationSender;

        internal Guid UserId => !Context.User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public NotificationHub(IMediator mediator,
        IMapper mapper,
        IDistributedCache cache,
        INotificationSender notificationSender)
        {
            _mediator = mediator;
            _mapper = mapper;
            _cache = cache;
            _notificationSender = notificationSender;
            _notificationSender.NotificationReceived += OnNotificationReceived;
        }
        void IDisposable.Dispose()
        {
            _notificationSender.NotificationReceived -= OnNotificationReceived;
        }
        public async Task ConnectToNotifications(ConnectNotificationSystemDto dto)
        {
            var connection = new NotificationSystemConnectionDto(UserId);
            var query = new GetUserNotificationListQuery
            {
                UserId = connection.UserId,
                Pagination = dto.Pagination,
                Sorting = dto.Sorting,
            };
            var notificationList = await _mediator.Send(query);
            var stringConnection = JsonSerializer.Serialize(connection);
            await _cache.SetStringAsync(Context.ConnectionId, stringConnection);

            await Clients
                    .Users(connection.UserId.ToString())
                    .ReceiveNotificationList(notificationList);
        }

        private async void OnNotificationReceived(object? sender, NotifyDto dto)
        {
            await Send(dto);   
        }

        private async Task Send(NotifyDto dto)
        {
            await Clients
                .Users(dto.UserId.ToString())
                .ReceiveNotification(dto.Notification);
        }
    }
}
