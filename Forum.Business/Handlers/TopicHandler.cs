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
        return await UnitOfWork.Topic.GetByIdAsync<Guid>(new Guid(topicId));
    }

    public async Task AddTopicAsync(Topic topic)
    {
        await UnitOfWork.Topic.AddAsync(topic);
    }
}