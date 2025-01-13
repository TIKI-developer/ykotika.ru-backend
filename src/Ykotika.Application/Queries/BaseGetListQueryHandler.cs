using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Extensions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Queries
{
    public class BaseGetListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetQueryHandler(dbContext, mapper)
    {
        protected static IQueryable<T> Sort<T>(IQueryable<T> query, string? sortBy, bool desc = false)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = desc
                ? query.OrderByDescending
                    (c => EF.Property<object>(c, sortBy.ToLower().FirstCharToUpper()))
                    : query.OrderBy(c => EF.Property<object>(c, sortBy.ToLower().FirstCharToUpper()));
            }

            return query;
        }
    }
}
