using Forum.Core.Models;

namespace Forum.Business.Services.Interfaces;

public interface ITopicService : IBaseService
{
    Task<Topic?> GetTopicByIdAsync(string topicId);
    Task AddTopicAsync(Topic topic);
}