using Forum.Business.Handlers;
using Forum.Business.Handlers.Interfaces;
using Forum.Infrastructure.DbInitializer;
using Forum.Infrastructure.Repository;
using Forum.Infrastructure.Repository.Interfaces;

namespace Forum.Web.Extensions;

public static class AppServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITopicHandler, TopicHandler>();
        services.AddScoped<IForumHandler, ForumHandler>();
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}