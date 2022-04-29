using Forum.Core.Models;

namespace Forum.Business.Services.Interfaces;

public interface IForumService
{
    Task<IEnumerable<Topic>> GetAllTopicsAsync();
}