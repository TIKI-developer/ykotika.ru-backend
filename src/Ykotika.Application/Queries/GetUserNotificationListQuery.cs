using MediatR;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetUserNotificationListQuery : IRequest<PagedList<NotificationItem>>
    {
        public required Guid UserId { get; set; }
        public required PaginationDto Pagination { get; set; }
        public required SortingDto Sorting { get; set; }
    }
}   
