using AutoMapper;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Queries
{
    public class BaseGetQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
    {
        protected IYkotikaDbContext _dbContext = dbContext;
        protected IMapper _mapper = mapper;
    }
}
