using Forum.Core.Models;

namespace Forum.Business.Services.Interfaces;

public interface ITopicService
{
    Task<Topic?> GetTopicByIdAsync(string topicId);
}