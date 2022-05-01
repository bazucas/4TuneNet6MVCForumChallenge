using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Repository;

/// <summary>
/// ApplicationUserRepository inherits <see cref="Repository{ApplicationUser}"/> T = <see cref="ApplicationUser"/> and extends <see cref="IApplicationUserRepository"/>
/// </summary>
/// <seealso cref="Repository{ApplicationUser}" />
/// <seealso cref="IApplicationUserRepository" />
public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    /// <summary>
    /// The database
    /// </summary>
    private readonly DbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUserRepository"/> class.
    /// </summary>
    /// <param name="db">The database.</param>
    public ApplicationUserRepository(DbContext db) : base(db)
    {
        _db = db;
    }
}