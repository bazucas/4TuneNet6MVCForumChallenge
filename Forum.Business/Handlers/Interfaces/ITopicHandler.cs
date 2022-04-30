﻿using Forum.Core.Models;

namespace Forum.Business.Handlers.Interfaces;

public interface ITopicHandler : IBaseHandler
{
    Task<Topic?> GetTopicByIdAsync(string topicId);
    Task AddTopicAsync(Topic topic);
}