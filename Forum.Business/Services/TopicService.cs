using Forum.Business.Services.Interfaces;
using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Services;

public class TopicService : ITopicService
{
    private readonly IUnitOfWork _unitOfWork;

    public TopicService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Topic?> GetTopicByIdAsync(string topicId)
    {
        return await _unitOfWork.Topic.GetByIdAsync<Guid>(new Guid(topicId));
    }
}