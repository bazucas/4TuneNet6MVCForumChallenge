using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    private readonly DbContext _db;

    public ApplicationUserRepository(DbContext db) : base(db)
    {
        _db = db;
    }
}