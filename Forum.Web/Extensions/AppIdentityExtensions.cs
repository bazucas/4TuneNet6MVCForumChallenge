using Forum.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Web.Extensions;

public static class AppIdentityExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, string connString)
    {
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>(); ;

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString));
        return services;
    }
}