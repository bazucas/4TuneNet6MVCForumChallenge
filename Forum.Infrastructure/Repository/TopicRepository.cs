using Forum.Core.Models;
using Forum.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Repository;

public class TopicRepository : Repository<Topic>, ITopicRepository
{
    public TopicRepository(DbContext db) : base(db)
    {

    }
}