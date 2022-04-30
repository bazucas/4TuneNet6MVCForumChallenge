using Forum.Core.Models;

namespace Forum.Business.Handlers.Interfaces;

public interface IForumHandler : IBaseHandler
{
    Task<IEnumerable<Topic>> GetAllTopicsWithUserInfoAsync();
}