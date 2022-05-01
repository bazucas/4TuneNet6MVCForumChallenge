namespace Forum.Infrastructure.Repository.Interfaces;

/// <summary>
/// UnitOfWork interface
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the topic.
    /// </summary>
    /// <value>
    /// The topic.
    /// </value>
    ITopicRepository Topic { get; }

    /// <summary>
    /// Gets the application user.
    /// </summary>
    /// <value>
    /// The application user.
    /// </value>
    IApplicationUserRepository ApplicationUser { get; }

    /// <summary>
    /// Saves the asynchronous.
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();
}

