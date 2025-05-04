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

    public class NotificationHub(IMediator mediator, IMapper mapper, IDistributedCache cache) : Hub<INotificationClient>, INotificationService
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IDistributedCache _cache = cache;

        internal Guid UserId => !Context.User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

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
        public async Task Send(NotifyDto dto)
        {
            await Clients
                .Users(dto.UserId.ToString())
                .ReceiveNotification(dto.Notification);
        }
    }
}
