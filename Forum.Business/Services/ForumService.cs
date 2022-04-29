using Forum.Business.Services.Interfaces;
using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Services;

public class ForumService : BaseService, IForumService
{
    public ForumService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }

    public async Task<IEnumerable<Topic>> GetAllTopicsAsync() => await UnitOfWork.Topic.GetAllAsync();
}