using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Handlers;

/// <summary>
/// Forum Handler inherits <see cref="BaseHandler"/>  and implements <see cref="IForumHandler"></see>
/// </summary>
/// <seealso cref="Forum.Business.Handlers.BaseHandler" />
/// <seealso cref="Forum.Business.Handlers.Interfaces.IForumHandler" />
public class ForumHandler : BaseHandler, IForumHandler
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForumHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    public ForumHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }

    /// <summary>
    /// Gets all topics with user information asynchronous.
    /// </summary>
    /// <returns>
    /// A List of <see cref="IEnumerable{Topic}" /> T = <see cref="Topic" />
    /// </returns>
    public async Task<IEnumerable<Topic>> GetAllTopicsWithUserInfoAsync() => await UnitOfWork.Topic.GetAllAsync(includeProperties: "ApplicationUser");
}