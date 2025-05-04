using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    internal class GetUserNotificationListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetListQueryHandler(dbContext, mapper), 
        IRequestHandler<GetUserNotificationListQuery, PagedList<NotificationItem>>
    {
        public async Task<PagedList<NotificationItem>> 
            Handle(GetUserNotificationListQuery request, CancellationToken cancellationToken)
        {
            var query =
                _dbContext
                .Notifications
                .AsQueryable()
                .Where(e => e.UserId == request.UserId);

            query = Sort(query);

            var queryItems = query
                .Include(e => e.Timestamps)
                .ProjectTo<NotificationItem>(_mapper.ConfigurationProvider);

            return await PagedList<NotificationItem>.CreateAsync(queryItems, request.Pagination.Page, request.Pagination.PageSize);
        }
    }
}
