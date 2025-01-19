using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Persistence
{
    public class DbInitializer(IPasswordHasher passwordHasher)
    {
        private IPasswordHasher _passwordHasher = passwordHasher;

        public void Initialize(YkotikaDbContext restaurantDbContext)
        {
            restaurantDbContext.Database.EnsureDeleted();
            restaurantDbContext.Database.EnsureCreated();

            CreateSuperUser(restaurantDbContext);
        }
        private void CreateSuperUser(YkotikaDbContext context)
        {
            var superUser = context.Users.FirstOrDefault(u => u.Email == "admin@ykotika.ru");
            if (superUser == null)
            {
                superUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@ykotika.ru",
                    Name = "admin",
                    PasswordHash = _passwordHasher.Generate("qwerty123"),
                    Timestamps = new Timestamps(),
                    Roles = [UserRole.Admin, UserRole.Verified, UserRole.Moderator, UserRole.Director],
                    ConfirmedPersonalDataProcessingPolicy = true
                };
                context.Users.Add(superUser);
                context.SaveChanges();
            }
        }

    }
}
