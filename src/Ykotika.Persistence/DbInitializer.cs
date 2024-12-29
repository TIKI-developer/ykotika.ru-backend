namespace Ykotika.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(YkotikaDbContext restaurantDbContext)
        {
            restaurantDbContext.Database.EnsureCreated();
        }
    }
}
