using Forum.Infrastructure.Context;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Topic = new TopicRepository(_db);
        ApplicationUser = new ApplicationUserRepository(_db);
    }
    public ITopicRepository Topic { get; }

    public IApplicationUserRepository ApplicationUser { get; }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}