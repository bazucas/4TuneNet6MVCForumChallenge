using Forum.Business.Handlers.Interfaces;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Handlers;

/// <summary>
/// Base Handler implements <see cref="IBaseHandler"></see>
/// </summary>
/// <seealso cref="Forum.Business.Handlers.Interfaces.IBaseHandler" />
public class BaseHandler : IBaseHandler
{
    /// <summary>
    /// The unit of work
    /// </summary>
    internal readonly IUnitOfWork UnitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    public BaseHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    /// <summary>
    /// Saves all asynchronous.
    /// </summary>
    public async Task SaveAllAsync()
    {
        await UnitOfWork.SaveAsync();
    }
}