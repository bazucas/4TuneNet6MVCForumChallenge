using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Repository;

/// <summary>
/// TopicRepository inherits <see cref="Repository{Topic}"/> T = <see cref="Topic"/> and extends <see cref="ITopicRepository"/>
/// </summary>
/// <seealso cref="Forum.Infrastructure.Repository.Repository&lt;Forum.Core.Models.Topic&gt;" />
/// <seealso cref="Forum.Infrastructure.Repository.Interfaces.ITopicRepository" />
public class TopicRepository : Repository<Topic>, ITopicRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TopicRepository"/> class.
    /// </summary>
    /// <param name="db">The database.</param>
    public TopicRepository(DbContext db) : base(db)
    {

    }
}