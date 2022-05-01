using Forum.Infrastructure.Context;

namespace Forum.Infrastructure.DbInitializer
{
    public interface IDbInitializer
    {
        Task Initialize(ApplicationDbContext? db);
    }
}
