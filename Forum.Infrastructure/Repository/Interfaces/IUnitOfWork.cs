namespace Forum.Infrastructure.Repository.Interfaces;

public interface IUnitOfWork
{
    ITopicRepository Topic { get; }

    IApplicationUserRepository ApplicationUser { get; }

    Task SaveAsync();
}

