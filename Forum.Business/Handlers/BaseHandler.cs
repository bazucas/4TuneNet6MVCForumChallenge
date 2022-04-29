using Forum.Business.Handlers.Interfaces;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Handlers;

public class BaseHandler : IBaseHandler
{
    internal readonly IUnitOfWork UnitOfWork;

    public BaseHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task SaveAllAsync()
    {
        await UnitOfWork.SaveAsync();
    }
}