using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ykotika.Application.Extensions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Queries
{
    public class BaseGetListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetQueryHandler(dbContext, mapper)
    {
        protected static IQueryable<T> Sort<T>(IQueryable<T> query, string? sortBy = "Timestamps_CreatedAt", bool desc = false)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var parameter = Expression.Parameter(typeof(T), "c");
                var propertyNames = sortBy.Split('_');
                Expression property = parameter;

                foreach (var propertyName in propertyNames)
                {
                    property = Expression.PropertyOrField(property, propertyName.ToLower().FirstCharToUpper());
                }

                var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
                query = desc ? query.OrderByDescending(lambda) : query.OrderBy(lambda);
            }
            else
            {
                query = desc ? query.OrderByDescending(c => EF.Property<object>(EF.Property<object>(c, "Timestamps"), "CreatedAt")) : query.OrderBy(c => EF.Property<object>(EF.Property<object>(c, "Timestamps"), "CreatedAt"));
            }

            return query;
        }
    }
}
