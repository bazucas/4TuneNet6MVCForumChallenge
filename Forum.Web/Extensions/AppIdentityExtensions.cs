using Forum.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Web.Extensions;

/// <summary>
/// AppIdentityExtensions
/// </summary>
public static class AppIdentityExtensions
{
    /// <summary>
    /// Extension method that receives <see cref="IServiceCollection"/> and adds the identity services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="connString">The connection string.</param>
    /// <returns>Returns </returns>
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, string connString)
    {
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>(); ;

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString));
        return services;
    }
}