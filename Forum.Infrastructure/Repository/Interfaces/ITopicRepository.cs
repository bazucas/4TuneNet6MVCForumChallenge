using Forum.Core.Models;

namespace Forum.Infrastructure.Repository.Interfaces;

/// <summary>
/// ApplicationUserRepository interface, extends <see cref="IRepository{ApplicationUser}"></see> T = <see cref="ApplicationUser"/>
/// </summary>
/// <seealso cref="Forum.Infrastructure.Repository.Interfaces.IRepository{ApplicationUser}" />
public interface IApplicationUserRepository : IRepository<ApplicationUser>
{

}