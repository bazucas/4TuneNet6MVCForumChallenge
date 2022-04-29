using Forum.Business.Services.Interfaces;
using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Services;

public class TopicService : BaseService, ITopicService
{
    public TopicService(IUnitOfWork unitOfWork) : base(unitOfWork)
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