using Microsoft.EntityFrameworkCore;
using ykotika.Application.Interfaces;

namespace ykotika.Persistence
{
    public class YkotikaDbContext(DbContextOptions<YkotikaDbContext> options) : DbContext(options), IYkotikaDbContext
    {

    }
}
