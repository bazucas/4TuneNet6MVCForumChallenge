using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Handlers;

public class TopicHandler : BaseHandler, ITopicHandler
{
    public TopicHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }

    public async Task<Topic?> GetTopicByIdAsync(string topicId)
    {
        return await UnitOfWork.Topic.GetFirstOrDefaultAsync(q => q.Id == new Guid(topicId), includeProperties: "ApplicationUser");
    }

    public async Task AddTopicAsync(Topic topic)
    {
        await UnitOfWork.Topic.AddAsync(topic);
    }

    public async Task DeleteTopicAsync(string topicId)
    {
        var topic = await UnitOfWork.Topic.GetByGuidIdAsync(new Guid(topicId));
        if (topic != null) UnitOfWork.Topic.Remove(topic);
    }

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