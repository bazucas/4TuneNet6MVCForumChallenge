using Forum.Core.Models;

namespace Forum.Business.Handlers.Interfaces;

/// <summary>
/// ForumHandler interface
/// </summary>
/// <seealso cref="Forum.Business.Handlers.Interfaces.IBaseHandler" />
public interface IForumHandler : IBaseHandler
{
    /// <summary>
    /// Gets all topics with user information asynchronous.
    /// </summary>
    /// <returns>A List of <see cref="IEnumerable{Topic}"/> T = <see cref="Topic"/></returns>
    Task<IEnumerable<Topic>> GetAllTopicsWithUserInfoAsync();
}