namespace Forum.Web.Extensions;

public static class AppSessionsExtensions
{
    public static IServiceCollection AddSessionsServices(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
            options.LogoutPath = "/Identity/Account/Logout";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(100);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        }); return services;
    }
}