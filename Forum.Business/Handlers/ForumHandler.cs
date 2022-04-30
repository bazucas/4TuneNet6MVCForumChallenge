using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Handlers;

public class ForumHandler : BaseHandler, IForumHandler
{
    public ForumHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }

    public async Task<IEnumerable<Topic>> GetAllTopicsWithUserInfoAsync() => await UnitOfWork.Topic.GetAllAsync(includeProperties: "ApplicationUser");
}