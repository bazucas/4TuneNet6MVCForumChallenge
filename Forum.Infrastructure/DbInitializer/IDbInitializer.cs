using Forum.Infrastructure.Context;

namespace Forum.Infrastructure.DbInitializer
{
    /// <summary>
    /// DbInitializer interface
    /// </summary>
    public interface IDbInitializer
    {
        /// <summary>
        /// Initializes the specified database.
        /// </summary>
        /// <param name="db">The database.</param>
        Task Initialize(ApplicationDbContext? db);
    }
}
