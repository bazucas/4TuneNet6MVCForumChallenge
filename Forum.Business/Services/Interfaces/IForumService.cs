using Forum.Core.Models;

namespace Forum.Business.Services.Interfaces;

public interface IForumService : IBaseService
{
    Task<IEnumerable<Topic>> GetAllTopicsAsync();
}