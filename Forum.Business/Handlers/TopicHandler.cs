using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Handlers;

/// <summary>
/// Topic Handler inherits <see cref="BaseHandler"/>  and implements <see cref="ITopicHandler"></see>
/// </summary>
/// <seealso cref="Forum.Business.Handlers.BaseHandler" />
/// <seealso cref="Forum.Business.Handlers.Interfaces.ITopicHandler" />
public class TopicHandler : BaseHandler, ITopicHandler
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TopicHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    public TopicHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }

    /// <summary>
    /// Gets the topic by identifier asynchronous.
    /// </summary>
    /// <param name="topicId">The topic identifier.</param>
    /// <returns>A nullable <see cref="Topic"/></returns>
    public async Task<Topic?> GetTopicByIdAsync(string topicId)
    {
        return await UnitOfWork.Topic.GetFirstOrDefaultAsync(q => q.Id == new Guid(topicId), includeProperties: "ApplicationUser");
    }

    /// <summary>
    /// Adds the topic asynchronous.
    /// </summary>
    /// <param name="topic">The topic.</param>
    public async Task AddTopicAsync(Topic topic)
    {
        await UnitOfWork.Topic.AddAsync(topic);
    }

    /// <summary>
    /// Deletes the topic asynchronous.
    /// </summary>
    /// <param name="topicId">The topic identifier.</param>
    public async Task DeleteTopicAsync(string topicId)
    {
        var topic = await UnitOfWork.Topic.GetByGuidIdAsync(new Guid(topicId));
        if (topic != null) UnitOfWork.Topic.Remove(topic);
    }

    /// <summary>
    /// Updates the topic asynchronous.
    /// </summary>
    /// <param name="topic">The topic.</param>
    public async Task UpdateTopicAsync(Topic topic)
    {
        var topicFromDb = await UnitOfWork.Topic.GetByGuidIdAsync(topic.Id);
        if (topicFromDb is not null)
        {
            topicFromDb.Title = topic.Title;
            topicFromDb.Description = topic.Description;
            UnitOfWork.Topic.Update(topicFromDb);
        }
    }
}