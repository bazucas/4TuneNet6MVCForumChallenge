using Forum.Core.Models;

namespace Forum.Infrastructure.Repository.Interfaces;

/// <summary>
/// TopicRepository interface, extends <see cref="IRepository{Topic}"/>T = <see cref="Topic"/>
/// </summary>
/// <seealso cref="Forum.Infrastructure.Repository.Interfaces.IRepository{Topic}" />
public interface ITopicRepository : IRepository<Topic>
{

}