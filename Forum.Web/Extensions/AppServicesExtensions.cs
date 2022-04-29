using Forum.Business.Services;
using Forum.Business.Services.Interfaces;
using Forum.Infrastructure.Context;
using Forum.Infrastructure.Repository;
using Forum.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Web.Extensions;

public static class AppServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DbContext, ApplicationDbContext>();
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IForumService, ForumService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //builder.Services.AddScoped<IDbInitializer, DbInitializer>();
        return services;
    }
}