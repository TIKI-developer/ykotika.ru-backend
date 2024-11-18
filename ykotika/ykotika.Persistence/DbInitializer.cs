namespace ykotika.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(YkotikaDbContext restaurantDbContext)
        {
            var created = restaurantDbContext.Database.EnsureCreated();
        }
    }
}
