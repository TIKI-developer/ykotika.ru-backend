using Ykotika.Application.Models;

namespace Ykotika.WebAPI.Models
{
    public record ConnectNotificationSystemDto(SortingDto Sorting, PaginationDto Pagination);
    public record NotificationSystemConnectionDto(Guid UserId);
}
