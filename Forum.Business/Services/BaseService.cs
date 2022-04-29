using Forum.Business.Services.Interfaces;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Business.Services;

public class BaseService : IBaseService
{
    internal readonly IUnitOfWork UnitOfWork;

    public BaseService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task SaveAllAsync()
    {
        await UnitOfWork.SaveAsync();
    }
}