using Forum.Infrastructure.Context;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Infrastructure.Repository;

/// <summary>
/// UnitOfWork class, implements <see cref="IUnitOfWork"/>
/// </summary>
/// <seealso cref="Forum.Infrastructure.Repository.Interfaces.IUnitOfWork" />
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// The database
    /// </summary>
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="db">The database.</param>
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Topic = new TopicRepository(_db);
        ApplicationUser = new ApplicationUserRepository(_db);
    }
    /// <summary>
    /// Gets the topic.
    /// </summary>
    /// <value>
    /// The topic.
    /// </value>
    public ITopicRepository Topic { get; }

    /// <summary>
    /// Gets the application user.
    /// </summary>
    /// <value>
    /// The application user.
    /// </value>
    public IApplicationUserRepository ApplicationUser { get; }

    /// <summary>
    /// Saves the asynchronous.
    /// </summary>
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}