using Forum.Business.Services.Interfaces;
using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Services;

public class ForumService : IForumService
{
    private readonly IUnitOfWork _unitOfWork;

    public ForumService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Topic>> GetAllTopicsAsync() => await _unitOfWork.Topic.GetAllAsync();
}