using Forum.Core.Models;

namespace Forum.Business.Handlers.Interfaces;

/// <summary>
/// Topic Handler interface
/// </summary>
/// <seealso cref="Forum.Business.Handlers.Interfaces.IBaseHandler" />
public interface ITopicHandler : IBaseHandler
{
    /// <summary>
    /// Gets the topic by identifier asynchronous.
    /// </summary>
    /// <param name="topicId">The topic identifier.</param>
    /// <returns>A nullable Topic <see cref="Topic"/></returns>
    Task<Topic?> GetTopicByIdAsync(string topicId);

    /// <summary>
    /// Adds the topic asynchronous.
    /// </summary>
    /// <param name="topic">The topic.</param>
    /// <returns></returns>
    Task AddTopicAsync(Topic topic);

    /// <summary>
    /// Deletes the topic asynchronous.
    /// </summary>
    /// <param name="topicId">The topic identifier.</param>
    /// <returns></returns>
    Task DeleteTopicAsync(string topicId);

    /// <summary>
    /// Updates the topic asynchronous.
    /// </summary>
    /// <param name="topic">The topic.</param>
    /// <returns></returns>
    Task UpdateTopicAsync(Topic topic);
}